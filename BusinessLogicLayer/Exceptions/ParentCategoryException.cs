using BusinessLogicLayer.Exceptions.Abstract;

namespace BusinessLogicLayer.Exceptions
{
    public class ParentCategoryException : IncorrectModelException
    {
        public ParentCategoryException(string message) : base(message) { }
    }
}
