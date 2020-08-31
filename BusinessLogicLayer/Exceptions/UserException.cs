using BusinessLogicLayer.Exceptions.Abstract;

namespace BusinessLogicLayer.Exceptions
{
    public class UserException : IncorrectModelException
    {
        public UserException(string message) : base(message) { }
    }
}
