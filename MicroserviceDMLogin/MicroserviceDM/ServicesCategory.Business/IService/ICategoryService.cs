using ServicesCategory.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCategory.Business.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoriesByIdAsync(int idCategory);
        Task<CategoryDto> CreateUpdateCategoryAsync(CategoryDto categoryDto);
        Task<bool> DeteleCategoryAsync(int idCategory);
    }
}
