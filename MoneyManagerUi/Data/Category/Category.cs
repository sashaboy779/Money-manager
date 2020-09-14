using System.ComponentModel.DataAnnotations;

namespace MoneyManagerUi.Data.Category
{
    public class Category
    {
        [Required(ErrorMessageResourceType = typeof(ModelsResources), 
            ErrorMessageResourceName = nameof(ModelsResources.CategoryNameRequired))]
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
