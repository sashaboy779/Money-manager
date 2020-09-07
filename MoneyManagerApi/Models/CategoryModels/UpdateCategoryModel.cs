using System.ComponentModel.DataAnnotations;

namespace MoneyManagerApi.Models.CategoryModels
{
    public class UpdateCategoryModel
    {
        public int CategoryId { get; set; }

        [Required (ErrorMessage = "CategoryNameRequired")]
        public string Name { get; set; }
    }
}
