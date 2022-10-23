using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.CategoryAPI.DbContexts;
using Services.CategoryAPI.Models;
using Services.CategoryAPI.Models.Dto;
using Services.CategoryAPI.Repository.Business;

namespace Services.CategoryAPI.Repository.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMapper _autoMapper;
        private readonly ApplicationDbContext _db;
       


        public CategoryRepository(IMapper autoMapper, ApplicationDbContext db)
        {
            _autoMapper = autoMapper;
            _db = db;
          
        }

        public async Task<CategoryDto> CreateUpdateCategoryAsync(CategoryDto categoryDto)
        {
            Category category = new Category();

            try
            {
                category  =  _db.DMCategoriesAPI.Where(c => c.CategoryID == categoryDto.CategoryID).FirstOrDefault();

                if (category.CategoryID > 0)
                {
                    _db.Update(category);
                  await  _db.SaveChangesAsync();

                }
                else
                {
                    _db.DMCategoriesAPI.Add(category);
                    await _db.SaveChangesAsync();
                }

               
            }
            catch (Exception ex)
            {
              //  _logger.LogError("Error al insertar en base de datos: {0}", ex.ToString());

                return new CategoryDto();
            }

            return _autoMapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeteleCategoryAsync(int idCategory)
        {
            Category category = new Category();

            try
            {
                category = _db.DMCategoriesAPI.Where(c => c.CategoryID == idCategory).FirstOrDefault();

                if (category.CategoryID > 0)
                {
                    _db.DMCategoriesAPI.Remove(category);
                    await _db.SaveChangesAsync();

                }            

            }
            catch (Exception ex)
            {
               // _logger.LogError("Error al eliminar en base de datos: {0}", ex.ToString());

                return false;
            }

            return true;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            List<Category> category = new List<Category>();

            try
            {
                category = await _db.DMCategoriesAPI.ToListAsync();

               

            }
            catch (Exception ex)
            {
              //  _logger.LogError("Error al traer los registros: {0}", ex.ToString());

               return new List<CategoryDto>();
            }

            return _autoMapper.Map<IList<CategoryDto>>(category);
        }

        public async Task<CategoryDto> GetCategoriesByIdAsync(int idCategory)
        {
           Category category = new Category();

            try
            {
                category =await  _db.DMCategoriesAPI.Where(c=>c.CategoryID == idCategory).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
              //  _logger.LogError("Error al traer los registros: {0}", ex.ToString());

                return new CategoryDto();
            }

            return _autoMapper.Map<CategoryDto>(category);
        }
    }
}
