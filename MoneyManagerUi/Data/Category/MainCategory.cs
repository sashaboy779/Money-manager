using System.Collections.Generic;

namespace MoneyManagerUi.Data.Category
{
    public class MainCategory : Category
    {
        public ICollection<Subcategory> Subcategories { get; set; }
    }
}
