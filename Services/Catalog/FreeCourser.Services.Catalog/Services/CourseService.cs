using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.CreateDtos;
using FreeCourse.Services.Catalog.Dtos.DefaultDtos;
using FreeCourse.Services.Catalog.Models;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared_.Dtos;
using MongoDB.Driver;

namespace FreeCourse.Services.Catalog.Services
{
	public class CourseService : ICourseService
	{
		private readonly IMongoCollection<Course> _courseCollection;

		private readonly IMongoCollection<Category> _categoryCollection;

		private readonly IMapper _mapper;

		public CourseService(IMapper mapper, IDatabaseSettings databaseSettings)
		{
			var client = new MongoClient(databaseSettings.ConnectionString);

			var database = client.GetDatabase(databaseSettings.DatabaseName);

			_courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);

			_categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

			_mapper = mapper;
		}

		public async Task<Response<List<CourseDto>>> GetAllAsync()
		{
			var courses = await _courseCollection.Find(course => true).ToListAsync();

			if (courses.Any())
			{
				foreach (var course in courses)
				{
					var category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();

					course.Category = category;
				}
			}
			else
			{
				courses = new List<Course>();
			}

			var coursesDto = _mapper.Map<List<CourseDto>>(courses);

			return Response<List<CourseDto>>.Success(coursesDto, 200);
		}

		public async Task<Response<CourseDto>> GetByIdAsync(string id)
		{
			var course = await _courseCollection.Find(course => course.Id == id).FirstOrDefaultAsync();

			if (course == null)
			{
				return Response<CourseDto>.Fail("Not Found", 404);
			}

			course.Category = await _categoryCollection.Find(category => category.Id == course.CategoryId).FirstOrDefaultAsync();

			var courseDto = _mapper.Map<CourseDto>(course);

			return Response<CourseDto>.Success(courseDto, 200);
		}

		public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
		{
			var courses = await _courseCollection.Find(course => course.UserId == userId).ToListAsync();

			if (courses.Any())
			{
				foreach (var course in courses)
				{
					var category = await _categoryCollection.Find(x => x.Id == course.CategoryId).FirstOrDefaultAsync();

					course.Category = category;
				}
			}
			else
			{
				courses = new List<Course>();
			}

			var coursesDto = _mapper.Map<List<CourseDto>>(courses);

			return Response<List<CourseDto>>.Success(coursesDto, 200);
		}

		public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto)
		{
			var course = _mapper.Map<Course>(courseCreateDto);

			course.CreationDate = DateTime.Now;

			await _courseCollection.InsertOneAsync(course);

			return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
		}

		public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseUpdateDto)
		{
			var course = _mapper.Map<Course>(courseUpdateDto);

			var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == courseUpdateDto.Id, course);

			if (result == null)
			{
				return Response<NoContent>.Fail("Course Not Found!", 404);
			}

			return Response<NoContent>.Success(204);
		}

		public async Task<Response<NoContent>> DeleteAsync(string id)
		{
			var result = await _courseCollection.DeleteOneAsync(course => course.Id == id);

			if (result.DeletedCount == 0)
			{
				return Response<NoContent>.Fail("Course Not Found!", 404);
			}

			return Response<NoContent>.Success(204);
		}
	}
}