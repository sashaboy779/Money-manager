using BusinessLogicLayer.Dto.UserDtos;
using System.Collections.Generic;

namespace BusinessLogicLayer.Dto.CategoryDtos
{
    public class MainCategoryDto : CategoryDto
    {
        public int UserId { get; set; }
        public virtual UserDto Owner { get; set; }

        public virtual ICollection<SubcategoryDto> Subcategories { get; set; } = new List<SubcategoryDto>();
    }
}
