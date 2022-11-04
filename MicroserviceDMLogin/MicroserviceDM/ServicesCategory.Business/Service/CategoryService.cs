using Microsoft.EntityFrameworkCore;
using ServicesCategory.Business.DTOs;
using ServicesCategory.Business.IService;
using ServicesCategory.DataAccess.Data;
using ServicesCategory.Models.Domain;
using ServicesCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesCategory.Business.Service
{
    public class CategoryService:ICategoryService
    {
        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {

            _db = db;

        }

        public async Task<CategoryDto> CreateUpdateCategoryAsync(CategoryDto categoryDto)
        {
            Category category = new Category();

            try
            {
                category = categoryDto.MapTo<Category>();


                if (categoryDto.CategoryID > 0)
                {
                    _db.Update(category);
                    await _db.SaveChangesAsync();

                }
                else
                {
                    _db.Categories.Add(category);
                    await _db.SaveChangesAsync();
                }


            }
            catch (Exception ex)
            {
                //  _logger.LogError("Error al insertar en base de datos: {0}", ex.ToString());

                return new CategoryDto();
            }


            return category.MapTo<CategoryDto>();

        }

        public async Task<bool> DeteleCategoryAsync(int idCategory)
        {
            Category category = new Category();

            try
            {
                category = _db.Categories.Where(c => c.CategoryID == idCategory).FirstOrDefault();

                if (category.CategoryID > 0)
                {
                    _db.Categories.Remove(category);
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
                category = await _db.Categories.ToListAsync();



            }
            catch (Exception ex)
            {
                //  _logger.LogError("Error al traer los registros: {0}", ex.ToString());

                return new List<CategoryDto>();
            }

            return category.MapTo<IList<CategoryDto>>();


        }

        public async Task<CategoryDto> GetCategoriesByIdAsync(int idCategory)
        {
            Category category = new Category();

            try
            {
                category = await _db.Categories.Where(c => c.CategoryID == idCategory).FirstOrDefaultAsync();

            }
            catch (Exception ex)
            {
                //  _logger.LogError("Error al traer los registros: {0}", ex.ToString());

                return new CategoryDto();
            }

            return category.MapTo<CategoryDto>();

        }
    }
}
