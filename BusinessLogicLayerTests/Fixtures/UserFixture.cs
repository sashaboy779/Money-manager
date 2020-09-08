using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Dto.UserDtos;
using DataAccessLayer.Entities;

namespace BusinessLogicLayerTest.Fixtures
{
    public class UserFixture
    {
        public User User { get; set; } = new User
        {
            Username = "Username"
        };

        public UserDto UserDto { get; set; } = new UserDto
        {
            Username = "Username"
        };

        public string Password { get; set; } = "Passw0rd!";

        public int ByteLength { get; set; } = 4;
        public CreatePasswordDto PasswordModel { get; set; } = new CreatePasswordDto
        {
            PasswordHash = new byte[] {1, 2, 3, 5},
            PasswordSalt = new byte[] {2, 4, 5, 7}
        };

        public UserDto UserForUpdateDto { get; set; } = new UserDto
        {
            Username = "New Username"
        };
        
        public User UserForUpdate { get; set; } = new User
        {
            Username = "New Username"
        };

        public int UpdatedByteLength { get; set; } = 3;

        public CreatePasswordDto PasswordModelForUpdate { get; set; } = new CreatePasswordDto
        {
            PasswordHash = new byte[] {1, 2, 3},
            PasswordSalt = new byte[] {2, 4, 5}
        };
    }
}