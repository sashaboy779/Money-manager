using System.Collections.Generic;

namespace BusinessLogicLayer.Dto
{
    public class ErrorsDto
    {
        public List<string> ModelErrors { get; set; }

        public ErrorsDto()
        {
            ModelErrors = new List<string>();
        }

        public void AddError(string error)
        {
            ModelErrors.Add(error);
        }
    }
}
