using System.Security.Claims;
using WebApp.Common;
using WebApp.Models;
using WebApp.Models.DTOs.Identity;
using WebApp.Services.IServices;

namespace WebApp.Services.Services
{
    public class IdentityService : BaseService, IIdentityService
    {
        private readonly IHttpClientFactory _clientFactory;

        public IdentityService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateTokenByUserAsync<T>(LoginDto loginDto)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.POST,
                Data = loginDto,
                url = StaticDetails.AuthenticaAPIBase + "api/v1/Authenticate/GenerateToken"

            });
        }

        public async Task<T> GetValidTokenAsync<T>(ClaimsIdentity identity)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.POST,
                Data = identity,
                url = StaticDetails.ProductAPIBase + "api/v1/Authenticate/ValidToken"

            });
        }

        public async Task<T> Login<T>(LoginDto loginDto)
        {
            return await this.SendAsync<T>(new ApiRequest
            {
                apiType = StaticDetails.ApiType.POST,
                Data = loginDto,
                url = StaticDetails.AuthenticaAPIBase + "api/v1/Authenticate/Login"

            });
        }
    }
}
