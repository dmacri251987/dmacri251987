using WebApp.Models;
using WebApp.Models.DTOs;

namespace WebApp.Services.IServices
{
    public interface IBaseService: IDisposable
    {
        ResponseDto responseModel { get; set; }     

        Task<T> SendAsync<T>(ApiRequest apiResquest);
    }
}
