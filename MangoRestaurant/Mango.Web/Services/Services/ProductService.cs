using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services.Services
{
    public class ProductService : BaseService, IProductService
    {


        private readonly IHttpClientFactory _clientFactory;

        public  ProductService(IHttpClientFactory clientFactory): base(clientFactory)
        {
            _clientFactory = clientFactory;
        }    

        public async Task<T> CreateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = SD.ApiType.POST,
                Data = productDto,
                url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }

        public  async Task<T> DeleteProductAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = SD.ApiType.DELETE,              
                url = SD.ProductAPIBase + "api/products/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = SD.ApiType.GET,
                url = SD.ProductAPIBase + "api/products/" + id,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = SD.ApiType.GET,
                url = SD.ProductAPIBase + "api/products/",
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = SD.ApiType.PUT,
                Data = productDto,
                url = SD.ProductAPIBase + "api/products",
                AccessToken = ""
            });
        }
    }
}
