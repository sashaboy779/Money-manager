using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using MoneyManagerUi.Services.Interfaces;
using MoneyManagerUi.Data.User;
using MoneyManagerUi.Infrastructure.Constants;

namespace MoneyManagerUi.Infrastructure
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IStorageService storageService;
        private readonly IUserService userService;

        public CustomAuthenticationStateProvider(IStorageService storageService,
            IUserService userService)
        {
            this.storageService = storageService;
            this.userService = userService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await storageService.GetItemAsync<string>(Configuration.TokenKey);
            ClaimsIdentity identity;

            if (!string.IsNullOrEmpty(accessToken))
            {
                var storagedUser = await storageService.GetItemAsync<UserModel>(Configuration.UserKey);
                identity = GetClaimsIdentity(storagedUser);
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task MarkUserAsAuthenticated(AccessToken accessToken)
        {
            var user = await userService.GetUserByAccessTokenAsync(accessToken.Token);

            await storageService.StoreItemAsync(Configuration.TokenKey, accessToken.Token);
            await storageService.StoreItemAsync(Configuration.UserKey, user);

            var identity = GetClaimsIdentity(user);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await storageService.RemoveItemAsync(Configuration.TokenKey);
            await storageService.RemoveItemAsync(Configuration.UserKey);
            await storageService.RemoveItemAsync(Configuration.WalletIdKey);

            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private ClaimsIdentity GetClaimsIdentity(UserModel user)
        {
            return new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            }, Configuration.AuthenticationType);
        }
    }
}
