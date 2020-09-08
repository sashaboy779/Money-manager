using System.Collections.Generic;
using BusinessLogicLayer.Dto.CategoryDtos;
using DataAccessLayer.Entities;

namespace BusinessLogicLayerTest.Fixtures
{
    public class CategoryServiceFixture
    {
        public User UserWithCategories { get; set; } = new User
        {
            UserId = 1,
            Categories = new List<MainCategory>
            {
                new MainCategory {Name = "Category 1", CategoryId = 1},
                new MainCategory {Name = "Category 2", CategoryId = 2, 
                    Subcategories = new List<Subcategory>
                    {
                        new Subcategory {CategoryId = 5, Name = "Subcategory 5"}
                    }
                },
                new MainCategory {Name = "Category 3", CategoryId = 3}
            }
        };

        public MainCategoryDto CreateCategoryDto { get; set; } = new MainCategoryDto
        {
            Name = "Create Category"
        };
        
        public MainCategory CreateCategory { get; set; } = new MainCategory
        {
            Name = "Create Category"
        };
        
        public MainCategoryDto CreateCategorySameName { get; set; } = new MainCategoryDto
        {
            Name = "Category 1"
        };
        
        public UpdateCategoryDto UpdateCategory { get; set; } = new UpdateCategoryDto
        {
            CategoryId = 2,
            Name = "Update category"
        };
        
        public UpdateCategoryDto UpdateSubCategory { get; set; } = new UpdateCategoryDto
        {
            CategoryId = 5,
            Name = "Update subcategory"
        };
    }
}