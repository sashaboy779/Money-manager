namespace BusinessLogicLayer.Exceptions.Abstract
{
    public abstract class IncorrectModelException : ServiceException
    {
        public IncorrectModelException(string message) : base(message)
        {
        }
    }
}
