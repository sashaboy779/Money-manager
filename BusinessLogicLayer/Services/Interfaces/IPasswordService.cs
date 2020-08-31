using BusinessLogicLayer.Dto;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IPasswordService
    {
        CreatePasswordDto CreatePasswordHash(string password);
        bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
    }
}
