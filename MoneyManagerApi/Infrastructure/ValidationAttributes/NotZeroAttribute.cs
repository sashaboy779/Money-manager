using System.ComponentModel.DataAnnotations;

namespace MoneyManagerApi.Infrastructure.ValidationAttributes
{
    public class NotZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (decimal) value != 0;
        }
    }
}