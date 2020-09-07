using System.ComponentModel.DataAnnotations;

namespace MoneyManagerApi.Models.CategoryModels
{
    public class CreateSubcategoryModel
    {
        [Required(ErrorMessage = ModelsResources.CategoryNameRequired)]
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }
    }
}
