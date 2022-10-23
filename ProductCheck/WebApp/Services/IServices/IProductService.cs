﻿using WebApp.Models.Dto.Product;

namespace WebApp.Services.IServices
{
    public interface IProductService:IBaseService
    {
        Task<T> GetProductsAsync<T>();
        Task<T> GetProductByIdAsync<T>(int id);
        Task<T> CreateProductAsync<T>(ProductDto product);
        Task<T> UpdateProductAsync<T>(ProductDto product);
        Task<T> DeleteProductAsync<T>(int id);        

    }
}
