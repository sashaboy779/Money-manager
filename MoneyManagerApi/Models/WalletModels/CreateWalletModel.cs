using System.ComponentModel.DataAnnotations;
using MoneyManagerApi.Infrastructure.ValidationAttributes;

namespace MoneyManagerApi.Models.WalletModels
{
    public class CreateWalletModel
    {
        [Required(ErrorMessage = ModelsResources.WalletNameRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ModelsResources.CurrencyRequired)]
        [NotUnknown(ErrorMessage = ModelsResources.NotUnknownCurrency)]
        public Currency? Currency { get; set; }

        [Required(ErrorMessage = ModelsResources.BalanceRequired)]
        public decimal? Balance { get; set; }
    }
}
