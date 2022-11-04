using WebApp.Models.DTOs.Product;

namespace WebApp.Services.IServices
{
    public interface IProductService
    {
        Task<T> GetProductsAsync<T>(string token = null);
        Task<T> GetProductByIdAsync<T>(int id, string token = null);
        Task<T> CreateProductAsync<T>(ProductDto product, string token = null);
        Task<T> UpdateProductAsync<T>(ProductDto product, string token = null);
        Task<T> DeleteProductAsync<T>(int id, string token = null);

    }
}
