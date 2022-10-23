using WebApp.Models;
using WebApp.Models.Dto;

namespace WebApp.Services.IServices
{
    public interface IBaseService:IDisposable
    {
        ResponseDto responseModel { get; set; }
        Task<T> SendAsync<T>(ApiResquest apiResquest);
    }
}
