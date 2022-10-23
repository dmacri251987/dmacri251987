using WebApp.Common;
using WebApp.Models;
using WebApp.Services.IServices;

namespace WebApp.Services.Services
{
    public class GatewayServices : BaseService, IGatewayService
    {

        private readonly IHttpClientFactory _clientFactory;

        public GatewayServices(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetProductCategoryByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiResquest
            {
                apiType = StaticDetails.ApiType.GET,
                url = StaticDetails.GatewayAPIBase + "api/GetProductCategoryById/" + id

            });
        }

        public async Task<T> GetProductsCategories<T>()
        {
            return await this.SendAsync<T>(new ApiResquest
            {
                apiType = StaticDetails.ApiType.GET,
                url = StaticDetails.GatewayAPIBase + "api/ProductCategories"

            });
        }
    }
}
