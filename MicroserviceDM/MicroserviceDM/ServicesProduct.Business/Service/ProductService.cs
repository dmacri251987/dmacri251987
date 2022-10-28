using Microsoft.EntityFrameworkCore;
using ServicesCommon;
using ServicesProduct.Business.DTOs;
using ServicesProduct.Business.IService;
using ServicesProduct.DataAccess.Data;
using ServicesProduct.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesProduct.Business.Service
{
    public class ProductService:IProductService
    {
        private readonly ApplicationDbContext _db;

        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ProductDto> CreateUpdateProductAsync(ProductDto productDto)
        {
            try
            {
                Product product = new Product();
                product = productDto.MapTo<Product>();

                if (productDto.ProductID > 0)
                {
                    _db.Products.Update(product);
                }
                else
                {

                    _db.Products.Add(product);
                }

                await _db.SaveChangesAsync();

                return productDto;
            }
            catch (Exception ex)
            {
                // _logger.LogError("Error al guardar en base de datos:{0} ", ex.ToString());
                return productDto;
            }

        }


        public async Task<bool> DeleteProductAsync(int productId)
        {

            try
            {
                if (productId == 0)
                {
                    return false;
                }

                Product product = await _db.Products.Where(u => u.ProductID == productId).FirstOrDefaultAsync();


                if (product != null && product.ProductID > 0)
                {
                    _db.Remove(product);
                    await _db.SaveChangesAsync();
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                //  _logger.LogError("Error al borrar en base de datos:{0} ", ex.ToString());
                return false;
            }

        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            try
            {

                Product product = await _db.Products.Where(x => x.ProductID == productId).FirstOrDefaultAsync();

                return product.MapTo<ProductDto>();


            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al buscar producto:{0} ", ex.ToString());
                return new ProductDto();

            }
        }

        public async Task<IEnumerable<ProductDto>> GetProductByIdCategoryAsync(int id)
        {
            try
            {

                List<Product> product = await _db.Products.Where(x => x.CategoryID == id).ToListAsync();
                return product.MapTo<List<ProductDto>>();

            }
            catch (Exception ex)
            {
                //_logger.LogError("Error al buscar producto:{0} ", ex.ToString());
                return new List<ProductDto>();

            }
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            try
            {

                List<Product> product = await _db.Products.ToListAsync();

                return product.MapTo<List<ProductDto>>();
            }
            catch (Exception ex)
            {
                //  _logger.LogError("Error al buscar producto:{0} ", ex.ToString());
                return new List<ProductDto>();

            }
        }

    }
}
