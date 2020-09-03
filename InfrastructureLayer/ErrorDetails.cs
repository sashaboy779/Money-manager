using Newtonsoft.Json;
using System.Collections.Generic;

namespace InfrastructureLayer
{
    public class ErrorDetails
    {
        public List<string> ModelErrors { get; set; }

        public ErrorDetails(string error)
        {
            ModelErrors = new List<string> { error };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
