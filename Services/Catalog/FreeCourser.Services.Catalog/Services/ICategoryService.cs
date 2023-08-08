using FreeCourse.Services.Catalog.Dtos.CreateDtos;
using FreeCourse.Services.Catalog.Dtos.DefaultDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Shared_.Dtos;

namespace FreeCourse.Services.Catalog.Services
{
	public interface ICategoryService
	{
		public Task<Response<List<CategoryDto>>> GetAllAsync();

		public Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto);

		public Task<Response<CategoryDto>> GetByIdAsync(string id);
	}
}