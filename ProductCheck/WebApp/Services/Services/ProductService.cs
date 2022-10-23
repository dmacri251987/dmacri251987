using WebApp.Common;
using WebApp.Models;
using WebApp.Models.Dto;
using WebApp.Models.Dto.Product;
using WebApp.Services.IServices;
using WebApp.Services.Services;

namespace WebApp.Services.Services
{
    public class ProductService :BaseService, IProductService
    {
       
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto product)
        {
            return await this.SendAsync<T>(new ApiResquest
            {
                apiType = StaticDetails.ApiType.POST,
                Data =product,
                url = StaticDetails.ProductAPIBase + "api/Product/CreateProduct"

            });
        }

        public async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiResquest
            {
                apiType = StaticDetails.ApiType.DELETE,               
                url = StaticDetails.ProductAPIBase + "api/Product/DeleteProduct/" + id

            });
        }

      

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiResquest
            {
                apiType = StaticDetails.ApiType.GET,
                url = StaticDetails.ProductAPIBase + "api/Product/GetProductById/" + id

            });
        }

        public async Task<T> GetProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiResquest
            {
                apiType = StaticDetails.ApiType.GET,
                url = StaticDetails.ProductAPIBase + "api/Product/GetProducts"

            });
        }       

        public async Task<T> UpdateProductAsync<T>(ProductDto product)
        {
            return await this.SendAsync<T>(new ApiResquest
            {
                apiType = StaticDetails.ApiType.POST,
                Data = product,
                url = StaticDetails.ProductAPIBase + "api/Product/UpdateProduct"

            });
        }

       
    }
}
