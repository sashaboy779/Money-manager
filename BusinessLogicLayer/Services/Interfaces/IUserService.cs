using BusinessLogicLayer.Dto.UserDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> AuthenticateAsync(string username, string password);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<UserDto> CreateAsync(UserDto userDto, string password);
        Task UpdateAsync(UserDto userDto, string password);
        Task DeleteAsync(int id);
    }
}
