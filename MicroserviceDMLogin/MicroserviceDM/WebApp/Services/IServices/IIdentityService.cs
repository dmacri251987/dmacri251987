using System.Security.Claims;
using WebApp.Models.DTOs.Identity;

namespace WebApp.Services.IServices
{
    public interface IIdentityService
    {
        Task<T> Login<T>(LoginDto loginDto);
        Task<T> RefreshTokenAsync<T>();
        Task<T> CreateTokenByUserAsync<T>(LoginDto loginDto);
        Task<T> GetValidTokenAsync<T>(ClaimsIdentity identity);
    }
}
