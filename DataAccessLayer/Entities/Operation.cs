using System;

namespace DataAccessLayer.Entities
{
    public class Operation
    {
        public int OperationId { get; set; }
        public DateTime OperationDate { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public decimal CurrentBalance { get; set; }

        public int WalletId { get; set; }
        public virtual Wallet Wallet { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
