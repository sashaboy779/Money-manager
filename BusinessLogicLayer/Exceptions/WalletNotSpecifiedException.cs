using BusinessLogicLayer.Exceptions.Abstract;

namespace BusinessLogicLayer.Exceptions
{
    public class WalletNotSpecifiedException : IncorrectModelException
    {
        public WalletNotSpecifiedException(string message) : base(message) { }
    }
}
