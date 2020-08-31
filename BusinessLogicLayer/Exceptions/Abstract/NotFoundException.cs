namespace BusinessLogicLayer.Exceptions.Abstract
{
    public class NotFoundException : ServiceException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
