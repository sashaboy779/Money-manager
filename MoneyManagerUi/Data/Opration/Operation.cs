using System;

namespace MoneyManagerUi.Data.Opration
{
    public class Operation
    {
        public int OperationId { get; set; }
        public DateTime OperationDate { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public Wallet.Wallet Wallet { get; set; }
        public Category.Category Category { get; set; }
    }
}
