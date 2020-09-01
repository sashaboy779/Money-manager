using BusinessLogicLayer.Exceptions.Abstract;

namespace BusinessLogicLayer.Exceptions
{
    public class CategoryNameException : IncorrectModelException
    {
        public CategoryNameException(string message) : base(message) { }
    }
}
