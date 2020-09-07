using MoneyManagerApi.Models.CategoryModels;
using System;

namespace MoneyManagerApi.Models.OperationModels
{
    public class WalletOperationModel
    {
        public int OperationId { get; set; }
        public DateTime OperationDate { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public Category Category { get; set; }
    }
}
