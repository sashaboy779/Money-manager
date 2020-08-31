using System;
using System.Linq;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Resources;
using BusinessLogicLayer.Services.Interfaces;

namespace BusinessLogicLayer.Services
{
    public class PasswordValidator : IPasswordValidator
    {
        private const int MinimalLength = 6;
        private const int MaximalLength = 25;

        public ErrorsDto Validate(string password)
        {
            var errors = new ErrorsDto();

            if (password.Length > MaximalLength)
                errors.AddError(String.Format(ServiceMessages.MaximalLengthError, MaximalLength));

            if (password.Length < MinimalLength)
               errors.AddError(String.Format(ServiceMessages.MinimalLengthError, MinimalLength));

            if (password.All(IsLetterOrDigit))
               errors.AddError(ServiceMessages.NonAlphanumericError);

            if (!password.Any(IsDigit))
               errors.AddError(ServiceMessages.DigitError);

            if (!password.Any(IsLower))
               errors.AddError(ServiceMessages.LowerError);

            if (!password.Any(IsUpper))
               errors.AddError(ServiceMessages.UpperError);

            return errors;
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsLower(char c)
        {
            return c >= 'a' && c <= 'z';
        }

        private bool IsUpper(char c)
        {
            return c >= 'A' && c <= 'Z';
        }

        private bool IsLetterOrDigit(char c)
        {
            return IsUpper(c) || IsLower(c) || IsDigit(c);
        }
    }
}
