using MoneyManagerUi.Infrastructure.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoneyManagerUi.Data.Opration
{
    public class CreateOperation
    {
        [NotZero(ErrorMessageResourceType = typeof(ModelsResources),
            ErrorMessageResourceName = nameof(ModelsResources.NotZero))]
        public decimal Amount { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public int WalletId { get; set; }
        public DateTime OperationDate { get; set; }
        public string Note { get; set; }
    }
}
