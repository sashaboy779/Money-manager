using System.Collections.Generic;
using System.Linq;

namespace MoneyManagerUi.Data
{
    public class Errors
    {
        public List<string> ModelErrors { get; set; } = new List<string>();

        public Errors()
        {
        }
        public Errors(string error)
        {
            ModelErrors.Add(error);
        }

        public Errors(IEnumerable<string> errors)
        {
            ModelErrors.AddRange(errors);
        }

        public bool Any()
        {
            return ModelErrors.Any();
        }
    }
}
