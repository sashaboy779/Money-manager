using BusinessLogicLayer.Exceptions.Abstract;

namespace BusinessLogicLayer.Exceptions
{
    public class OperationNotFoundException : NotFoundException
    {
        public OperationNotFoundException(string message) : base(message) { }
    }
}
