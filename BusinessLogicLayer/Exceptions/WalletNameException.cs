using BusinessLogicLayer.Exceptions.Abstract;

namespace BusinessLogicLayer.Exceptions
{
    public class WalletNameException : IncorrectModelException
    {
        public WalletNameException(string message) : base(message) { }
    }
}
