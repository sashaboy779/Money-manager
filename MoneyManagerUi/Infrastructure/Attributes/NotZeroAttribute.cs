using System.ComponentModel.DataAnnotations;

namespace MoneyManagerUi.Infrastructure.Attributes
{
    public class NotZeroAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (decimal)value != 0;
        }
    }
}
