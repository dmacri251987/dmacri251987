using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.ProductAPI.DbContexts;
using Services.ProductAPI.Models;
using Services.ProductAPI.Models.Dto;
using Services.ProductAPI.Repository.Business;

namespace Services.ProductAPI.Repository.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _autoMapper;
      

        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _autoMapper = mapper;
            
        }

        public async Task<ProductDto> CreateUpdateProductAsync(ProductDto productDto)
        {
            try
            {
                Product product = _autoMapper.Map<ProductDto, Product>(productDto);

                if (productDto.ProductID > 0)
                {
                    _db.DMProductsAPI.Update(product);
                }
                else
                {
                    _db.DMProductsAPI.Add(product);
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

                Product product = await _db.DMProductsAPI.Where(u => u.ProductID == productId).FirstOrDefaultAsync();


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

                Product product = await _db.DMProductsAPI.Where(x => x.ProductID == productId).FirstOrDefaultAsync();
                return _autoMapper.Map<ProductDto>(product);

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

                List<Product> product = await _db.DMProductsAPI.Where(x => x.CategoryID == id).ToListAsync();
                return _autoMapper.Map<List<ProductDto>>(product);

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

                List<Product> product = await _db.DMProductsAPI.ToListAsync();

                return _autoMapper.Map<List<ProductDto>>(product);
            }
            catch (Exception ex)
            {
              //  _logger.LogError("Error al buscar producto:{0} ", ex.ToString());
                return new List<ProductDto>();

            }
        }
    }
}
