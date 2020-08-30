using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual IEnumerable<Wallet> Wallets { get; set; } = new List<Wallet>();
        public virtual IEnumerable<MainCategory> Categories { get; set; } = new List<MainCategory>();
    }
}
