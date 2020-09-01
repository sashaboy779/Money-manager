namespace BusinessLogicLayer.Dto.WalletDtos
{
    public class UpdateWalletDto
    {
        public string Name { get; set; }
        public CurrencyDto Currency { get; set; }
        public decimal Balance { get; set; }
    }
}
