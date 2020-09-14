using MoneyManagerUi.Infrastructure.Constants;
using System.Collections.Generic;

namespace MoneyManagerUi.Shared
{
    public partial class UniqueName
    {
        public static IDictionary<string, object> AddClassesForName(string nameError)
        {
            var classes = CssConstants.Form;
            if (!string.IsNullOrEmpty(nameError))
            {
                classes += " " + CssConstants.Invalid;
            }

            return new Dictionary<string, object> { { CssConstants.Class, classes } };
        }
    }
}
