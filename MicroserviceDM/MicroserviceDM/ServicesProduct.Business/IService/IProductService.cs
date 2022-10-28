using ServicesProduct.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesProduct.Business.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int productId);
        Task<ProductDto> CreateUpdateProductAsync(ProductDto productDto);
        Task<bool> DeleteProductAsync(int productId);
        Task<IEnumerable<ProductDto>> GetProductByIdCategoryAsync(int id);
    }
}
