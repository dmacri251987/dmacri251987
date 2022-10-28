using WebApp.Models.DTOs.Product;

namespace WebApp.Services.IServices
{
    public interface IProductService
    {
        Task<T> GetProductsAsync<T>();
        Task<T> GetProductByIdAsync<T>(int id);
        Task<T> CreateProductAsync<T>(ProductDto product);
        Task<T> UpdateProductAsync<T>(ProductDto product);
        Task<T> DeleteProductAsync<T>(int id);

    }
}
