using System.ComponentModel.DataAnnotations;
using MoneyManagerApi.Models.WalletModels;

namespace MoneyManagerApi.Infrastructure.ValidationAttributes
{
    public class NotUnknownAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            
            return (Currency) value != Currency.Unknown;
        }
    }
}