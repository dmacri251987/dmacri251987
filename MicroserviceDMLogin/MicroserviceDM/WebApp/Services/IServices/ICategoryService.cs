using WebApp.Models.DTOs.Category;

namespace WebApp.Services.IServices
{
    public interface ICategoryService
    {
        Task<T> GetCategoriesAsync<T>(string token = null);
        Task<T> GetCategoryByIdAsync<T>(int id, string token = null);
        Task<T> CreateCategoryAsync<T>(CategoryDto category, string token = null);
        Task<T> UpdateCategoryAsync<T>(CategoryDto category, string token = null);
        Task<T> DeleteCategoryAsync<T>(int id, string token = null);
    }
}
