using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Resources;
using BusinessLogicLayer.Services.Interfaces;
using System;

namespace BusinessLogicLayer.Services
{
    public class PasswordService : IPasswordService
    {
        public CreatePasswordDto CreatePasswordHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            return new CreatePasswordDto
            {
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
            };
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64) throw new ArgumentException(ServiceMessages.InvalidPasswordHashLength,
                nameof(storedHash));
            if (storedSalt.Length != 128) throw new ArgumentException(ServiceMessages.InvalidPasswordSaltLength,
                nameof(storedSalt));

            using var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != storedHash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
