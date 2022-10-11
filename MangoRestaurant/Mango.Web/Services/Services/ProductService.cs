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

        public async Task<T> CreateProductAsync<T>(ProductDto productDto, string token)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = Common.SD.ApiType.POST,
                Data = productDto,
                url = Common.SD.ProductAPIBase + "api/products",
                AccessToken = token
            });
        }

        public  async Task<T> DeleteProductAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = Common.SD.ApiType.DELETE,              
                url = Common.SD.ProductAPIBase + "api/products/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllProductByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = Common.SD.ApiType.GET,
                url = Common.SD.ProductAPIBase + "api/products/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllProductsAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = Common.SD.ApiType.GET,
                url = Common.SD.ProductAPIBase + "api/products/",
                AccessToken = token
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDto productDto, string token)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = Common.SD.ApiType.PUT,
                Data = productDto,
                url = Common.SD.ProductAPIBase + "api/products",
                AccessToken = token
            });
        }
    }
}
