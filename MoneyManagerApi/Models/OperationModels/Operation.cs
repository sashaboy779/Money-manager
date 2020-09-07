using MoneyManagerApi.Models.CategoryModels;
using MoneyManagerApi.Models.WalletModels;
using System;

namespace MoneyManagerApi.Models.OperationModels
{
    public class Operation
    {
        public int OperationId { get; set; }
        public DateTime OperationDate { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public Wallet Wallet { get; set; }
        public Category Category { get; set; }
    }
}
