using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.CreateDtos;
using FreeCourse.Services.Catalog.Dtos.DefaultDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared_.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IMongoCollection<Category> _categoryCollection;

		private readonly IMapper _mapper;

		public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
		{
			var client = new MongoClient(databaseSettings.ConnectionString);

			var database = client.GetDatabase(databaseSettings.DatabaseName);

			_categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

			_mapper	= mapper;
		}

		public async Task<Response<List<CategoryDto>>> GetAllAsync()
		{
			var categories = await _categoryCollection.Find(category => true).ToListAsync();

			var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

			return Response<List<CategoryDto>>.Success(categoryDtos, 200);
		}

		public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
		{
			var category = _mapper.Map<Category>(categoryCreateDto);
			
			await _categoryCollection.InsertOneAsync(category);

			var categoryDtoWithId = _mapper.Map<CategoryDto>(category);

			return Response<CategoryDto>.Success(categoryDtoWithId, 200);
		}

		public async Task<Response<CategoryDto>> GetByIdAsync(string id)
		{
			var category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

			if (category == null)
			{
				return Response<CategoryDto>.Fail("Category Not Found!", 404);
			}

			var categoryDto = _mapper.Map<CategoryDto>(category);

			return Response<CategoryDto>.Success(categoryDto, 200);
		}
	}
}