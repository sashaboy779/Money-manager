using System;
using System.ComponentModel.DataAnnotations;
using MoneyManagerApi.Infrastructure.ValidationAttributes;

namespace MoneyManagerApi.Models.OperationModels
{
    public class CreateOperationModel
    {
        [NotZero(ErrorMessage = ModelsResources.NotZero)]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = ModelsResources.CategoryIdRequired)]
        public int? CategoryId { get; set; }

        public int WalletId { get; set; }
        public DateTime OperationDate { get; set; }
        public string Note { get; set; }
    }
}
