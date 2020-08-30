using DataAccessLayer.Entities.Enums;
using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public decimal Balance { get; set; }

        public virtual IEnumerable<Operation> Operations { get; set; } = new List<Operation>();

        public int UserId { get; set; }
        public virtual User Owner { get; set; }
    }
}
