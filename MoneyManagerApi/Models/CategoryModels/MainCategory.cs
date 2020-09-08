using System.Collections.Generic;

namespace MoneyManagerApi.Models.CategoryModels
{
    public class MainCategory : Category
    {
        public ICollection<Subcategory> Subcategories { get; set; }
    }
}
