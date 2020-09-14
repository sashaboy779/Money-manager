using MoneyManagerUi.Data;
using System;

namespace MoneyManagerUi.Infrastructure.Exceptions
{
    public class ModelErrorException : Exception
    {
        public Errors Errors { get; }

        public ModelErrorException(Errors errors)
        {
            Errors = errors;
        }
    }
}
