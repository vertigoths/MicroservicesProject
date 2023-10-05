using FreeCourse.Shared_.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput input)
        {
            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("courses", input);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return successResponse.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("courses");

            if (!response.IsSuccessStatusCode) 
            {
                return null;
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return successResponse.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return successResponse.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return successResponse.Data;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput input)
        {
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", input);

            return response.IsSuccessStatusCode;
        }
    }
}
