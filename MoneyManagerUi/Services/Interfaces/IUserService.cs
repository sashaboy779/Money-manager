using MoneyManagerUi.Data.User;
using System.Threading.Tasks;

namespace MoneyManagerUi.Services.Interfaces
{
    public interface IUserService
    {
        public Task<AccessToken> LoginAsync(AuthenticateModel user);
        public Task<UserModel> RegisterUserAsync(RegisterUserModel user);
        public Task<UserModel> GetUserByAccessTokenAsync(string accessToken);
    }
}
