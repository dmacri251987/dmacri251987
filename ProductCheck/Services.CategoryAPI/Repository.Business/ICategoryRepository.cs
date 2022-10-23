using Services.CategoryAPI.Models.Dto;

namespace Services.CategoryAPI.Repository.Business
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoriesByIdAsync(int idCategory);
        Task<CategoryDto> CreateUpdateCategoryAsync(CategoryDto categoryDto);        
        Task<bool> DeteleCategoryAsync(int idCategory);
    }
}
