using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.Dto;
using BusinessLogicLayer.Dto.CategoryDtos;
using BusinessLogicLayer.Services.Interfaces;
using MoneyManagerApi.Infrastructure.Constants;
using MoneyManagerApi.Models.CategoryModels;
using MoneyManagerApi.Models.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoneyManagerApi.Controllers
{
    [Route(Routes.Controller)]
    [ApiController]
    [Authorize]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService, IMapper mapper, IUriService uriService) 
            : base(mapper, uriService)
        {
            this.categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync(CreateCategoryModel category)
        {
            var userId = GetUserId();
            var categoryDto = Mapper.Map<MainCategoryDto>(category);

            var createdCategoryDto = await categoryService.CreateCategoryAsync(userId, categoryDto);
            var createdCategory = Mapper.Map<MainCategory>(createdCategoryDto);

            return CreatedAtRoute(
                Routes.ShowCategory,
                new { id = createdCategory.CategoryId },
                createdCategory);
        }

        [HttpPost(Routes.Id)]
        public async Task<IActionResult> CreateSubcategoryAsync(int id, CreateSubcategoryModel subcategory)
        {
            subcategory.ParentCategoryId = id;
            var userId = GetUserId();
            var subcategoryDto = Mapper.Map<SubcategoryDto>(subcategory);

            var createdSubcategoryDto = await categoryService.CreateSubcategoryAsync(
                userId, 
                subcategory.ParentCategoryId,
                subcategoryDto);

            var createdSubcategory = Mapper.Map<MainCategory>(createdSubcategoryDto);

            return CreatedAtRoute(
                Routes.ShowCategory,
                new { id = createdSubcategory.CategoryId },
                createdSubcategory);
        }

        [HttpGet(Routes.Id, Name = Routes.ShowCategory)]
        public async Task<IActionResult> ShowCategoryAsync(int id)
        {
            var categoryDto = await categoryService.GetCategoryAsync(GetUserId(), id);
            return Ok(Mapper.Map<MainCategory>(categoryDto));
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllCategoriesAsync([FromQuery] PaginationQuery pagination)
        {
            var userId = GetUserId();
            var filter = Mapper.Map<PaginationFilter>(pagination);
            var categoryDto = await categoryService.GetAllCategoriesAsync(userId, filter);

            if (!categoryDto.Any())
            {
                return NotFound();
            }

            var categories = Mapper.Map<IEnumerable<MainCategory>>(categoryDto);
            return IsPagingSpecified(pagination) ? Ok(CreatePagedResponse(pagination, categories)) : Ok(categories);
        }

        [HttpPut(Routes.Id)]
        public async Task<IActionResult> UpdateCategoryAsync(int id, UpdateCategoryModel category)
        {
            category.CategoryId = id;
            var updateCategoryDto = Mapper.Map<UpdateCategoryDto>(category);
            await categoryService.UpdateCategoryAsync(GetUserId(), updateCategoryDto);

            return NoContent();
        }

        [HttpDelete(Routes.Id)]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            await categoryService.DeleteCategoryAsync(GetUserId(), id);
            
            return NoContent();
        }
    }
}
