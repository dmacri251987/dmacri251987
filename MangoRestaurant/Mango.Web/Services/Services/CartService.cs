using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services.Services
{
    public class CartService : BaseService, ICartService
    {

        private readonly IHttpClientFactory _clientFactory;

        public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = Common.SD.ApiType.POST,
                Data = cartDto,
                url = Common.SD.ShoppingCartAPIBase + "api/cart/AddCart",
                AccessToken = token
            });
        }

        public async Task<T> ApplyCouponAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = Common.SD.ApiType.POST,
                Data = cartDto,
                url = Common.SD.ShoppingCartAPIBase + "api/cart/ApplyCoupon",
                AccessToken = token
            });
        }

        public async Task<T> GetCartByUserIdAsync<T>(string user, string token = null)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {

                apiType = Common.SD.ApiType.GET,
                url = Common.SD.ShoppingCartAPIBase + "api/cart/getCart/" + user,
                AccessToken = token

            });
        }

        public async Task<T> RemoveCouponAsync<T>(string userId, string token = null)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = Common.SD.ApiType.POST,
                Data = userId,
                url = Common.SD.ShoppingCartAPIBase + "api/cart/RemoveCoupon",
                AccessToken = token
            });
        }

        public async Task<T> RemoveFromCartAsync<T>(int cartId, string token = null)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = Common.SD.ApiType.DELETE,
                Data = cartId,
                url = Common.SD.ShoppingCartAPIBase + "api/cart/RemoveCart",
                AccessToken = token
            });
        }

        public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {
                apiType = Common.SD.ApiType.PUT,
                Data = cartDto,
                url = Common.SD.ShoppingCartAPIBase + "api/cart/UpdateCart",
                AccessToken = token
            });
        }
    }
}
