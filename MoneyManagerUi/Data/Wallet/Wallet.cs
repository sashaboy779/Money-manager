using System.ComponentModel.DataAnnotations;

namespace MoneyManagerUi.Data.Wallet
{
    public class Wallet
    {
        [Required(ErrorMessageResourceType = typeof(ModelsResources), 
            ErrorMessageResourceName = nameof(ModelsResources.WalletNameRequired))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelsResources), 
            ErrorMessageResourceName = nameof(ModelsResources.CurrencyRequired))]
        public Currency Currency { get; set; }

        [Required(ErrorMessageResourceType = typeof(ModelsResources), 
            ErrorMessageResourceName = nameof(ModelsResources.BalanceRequired))]
        public decimal Balance { get; set; }
        public int UserId { get; set; }
        public int WalletId { get; set; }
    }
}
