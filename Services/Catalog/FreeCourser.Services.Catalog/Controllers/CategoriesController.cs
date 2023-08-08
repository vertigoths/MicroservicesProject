using FreeCourse.Services.Catalog.Dtos.CreateDtos;
using FreeCourse.Services.Catalog.Dtos.DefaultDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Services;
using FreeCourse.Shared_.ControllerBases;
using FreeCourse.Shared_.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Catalog.Controllers
{
	public class CategoriesController : CustomControllerBase
	{
		private readonly ICategoryService _categoryService;

		public CategoriesController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var response = await _categoryService.GetAllAsync();

			return CreateActionResultInstance(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(string id)
		{
			var response = await _categoryService.GetByIdAsync(id);

			return CreateActionResultInstance(response);
		}

		[HttpPost]
		public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
		{
			var response = await _categoryService.CreateAsync(categoryCreateDto);

			return CreateActionResultInstance(response);
		}
	}
}