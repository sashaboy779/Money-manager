using System.Collections.Generic;
using System.Linq;

namespace MoneyManagerApi.Models
{
    public class Errors
    {
        public List<string> ModelErrors { get; set; }

        public Errors()
        {
            ModelErrors = new List<string>();
        }

        public Errors(string error)
        {
            ModelErrors = new List<string>
            {
                error
            };
        }

        public bool Any()
        {
            return ModelErrors.Any();
        }
    }
}
