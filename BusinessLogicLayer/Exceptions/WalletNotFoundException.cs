using BusinessLogicLayer.Exceptions.Abstract;

namespace BusinessLogicLayer.Exceptions
{
    public class WalletNotFoundException : NotFoundException
    {
        public WalletNotFoundException(string message) : base(message) { }
    }
}
