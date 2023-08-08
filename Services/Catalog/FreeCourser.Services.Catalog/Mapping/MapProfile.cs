using AutoMapper;
using FreeCourse.Services.Catalog.Dtos.CreateDtos;
using FreeCourse.Services.Catalog.Dtos.DefaultDtos;
using FreeCourse.Services.Catalog.Models;

namespace FreeCourse.Services.Catalog.Mapping
{
	public class MapProfile : Profile
	{
		public MapProfile()
		{
			CreateMap<Course, CourseDto>().ReverseMap();

			CreateMap<Category, CategoryDto>().ReverseMap();

			CreateMap<Feature, FeatureDto>().ReverseMap();

			CreateMap<Course, CourseCreateDto>().ReverseMap();

			CreateMap<Course, CourseUpdateDto>().ReverseMap();

			CreateMap<CategoryCreateDto, Category>().ReverseMap();
		}
	}
}
