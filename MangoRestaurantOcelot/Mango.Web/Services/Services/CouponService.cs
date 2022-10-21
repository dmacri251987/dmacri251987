using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services.Services
{
    public class CouponService :BaseService, ICouponService
    {

        private readonly IHttpClientFactory _clientFactory;

        public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<T> GetCoupon<T>(string couponCode, string token = null)
        {
            return await this.SendAsync<T>(new ApiResquest()
            {

                apiType = Common.SD.ApiType.GET,
                url = Common.SD.CouponAPIBase + "api/coupon/" + couponCode,
                AccessToken = token

            });

        }
    }
}
