using System.ComponentModel.DataAnnotations;

namespace MoneyManagerApi.Models.CategoryModels
{
    public class CreateCategoryModel
    {
        [Required(ErrorMessage = ModelsResources.CategoryNameRequired)]
        public string Name { get; set; }
    }
}
