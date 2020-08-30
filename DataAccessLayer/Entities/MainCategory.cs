using System.Collections.Generic;

namespace DataAccessLayer.Entities
{
    public class MainCategory : Category
    {
        public int UserId { get; set; }
        public virtual User Owner { get; set; }

        public virtual ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
    }
}
