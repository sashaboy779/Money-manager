namespace BusinessLogicLayer.Dto.CategoryDtos
{
    public class SubcategoryDto : CategoryDto
    {
        public int ParentId { get; set; }
        public virtual MainCategoryDto Parent { get; set; }
    }
}
