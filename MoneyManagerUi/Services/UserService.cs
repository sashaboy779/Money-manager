using MoneyManagerUi.Data.User;
using MoneyManagerUi.Infrastructure;
using MoneyManagerUi.Infrastructure.Constants;
using MoneyManagerUi.Infrastructure.Extensions;
using MoneyManagerUi.Services.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoneyManagerUi.Services
{
    public class UserService : IUserService
    {
        private readonly IExpanseManagerClient apiClient;

        public UserService(IExpanseManagerClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<AccessToken> LoginAsync(AuthenticateModel user)
        {
            return await SendPostRequest<AuthenticateModel, AccessToken>(ApiRoutes.SignIn, user);
        }

        public async Task<UserModel> RegisterUserAsync(RegisterUserModel user)
        {
            return await SendPostRequest<RegisterUserModel, UserModel>(ApiRoutes.Register, user);
        }

        public async Task<UserModel> GetUserByAccessTokenAsync(string accessToken)
        {
            apiClient
                .CreateRequestMessage(HttpMethod.Get, ApiRoutes.UserAccount)
                .AddAuthorization(accessToken);

            var response = await apiClient.SendRequestAsync();
            return await response.TryFetchContentAsync<UserModel>();
        }

        private async Task<TResponse> SendPostRequest<TContent, TResponse>(string uri, TContent content)
        {
            apiClient
                .CreateRequestMessage(HttpMethod.Post, uri)
                .AddJsonContent(content);

            var response = await apiClient.SendRequestAsync();
            return await response.TryFetchContentAsync<TResponse>();
        }
    }
}
