using WebApp.Common;
using WebApp.Models;
using WebApp.Models.DTOs.Product;
using WebApp.Services.IServices;

namespace WebApp.Services.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateProductAsync<T>(ProductDto product, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.POST,
                Data = product,
                url = StaticDetails.ProductAPIBase + "api/v1/Product/CreateProduct",
                AccessToken = token

            });
        }

        public async Task<T> DeleteProductAsync<T>(int id, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.DELETE,
                url = StaticDetails.ProductAPIBase + "api/v1/Product/DeleteProduct/" + id,
                AccessToken = token

            });
        }



        public async Task<T> GetProductByIdAsync<T>(int id, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.GET,
                url = StaticDetails.ProductAPIBase + "api/v1/Product/GetProductById/" + id,
                AccessToken = token

            });
        }



        public async Task<T> GetProductsAsync<T>(string token = null)
        {
           return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.GET,
                url = StaticDetails.ProductAPIBase + "api/v1/Product/GetProducts",
                AccessToken = token

            });
        }



        public async Task<T> UpdateProductAsync<T>(ProductDto product, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.PUT,
                Data = product,
                url = StaticDetails.ProductAPIBase + "api/v1/Product/UpdateProduct",
                AccessToken = token

            });
        }

    }
}
