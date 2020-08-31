using BusinessLogicLayer.Exceptions.Abstract;

namespace BusinessLogicLayer.Exceptions
{
    public class CategoryNotFoundException : NotFoundException
    {
        public CategoryNotFoundException(string message) : base(message) { }
    }
}
