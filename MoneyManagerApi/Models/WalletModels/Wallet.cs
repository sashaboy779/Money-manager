namespace MoneyManagerApi.Models.WalletModels
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }
    }
}
