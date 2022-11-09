using WebApp.Common;
using WebApp.Models;
using WebApp.Models.DTOs.Category;
using WebApp.Services.IServices;

namespace WebApp.Services.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly IHttpClientFactory _clientFactory;

        public CategoryService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateCategoryAsync<T>(CategoryDto category, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.POST,
                Data = category,
                url = StaticDetails.CategoryAPIBase + "api/v1/Category/CreateCategory",
                AccessToken = token
            });
        }

        public async Task<T> DeleteCategoryAsync<T>(int id, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.DELETE,
                url = StaticDetails.CategoryAPIBase + "api/v1/Category/DeleteCategory/" + id,
                AccessToken = token

            });
        }

        public async Task<T> GetCategoriesAsync<T>(string token = null)
        {

            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.GET,
                url = StaticDetails.CategoryAPIBase + "api/v1/Category/GetCategories",
                AccessToken = token

            });
        }

        public async Task<T> GetCategoryByIdAsync<T>(int id, string token = null)
        {

            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.GET,
                url = StaticDetails.CategoryAPIBase + "api/v1/Category/GetCategoryById/" + id,
                AccessToken = token

            });
        }

        public async Task<T> UpdateCategoryAsync<T>(CategoryDto category, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.PUT,
                Data = category,
                url = StaticDetails.CategoryAPIBase + "api/v1/Category/UpdateCategory",
                AccessToken = token

            });
        }
    }
}
