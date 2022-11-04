using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using WebApp.Models;
using WebApp.Models.DTOs;
using WebApp.Services.IServices;

namespace WebApp.Services.Services
{
    public class BaseService:IBaseService
    {
        public ResponseDto responseModel { get; set; }
        public IHttpClientFactory httpClient { get; set; }


        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new ResponseDto();
            this.httpClient = httpClient;
        }

        public async Task<T> SendAsync<T>(ApiRequest apiResquest)
        {
            try
            {
                var client = httpClient.CreateClient("MangoApi");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiResquest.url);
                client.DefaultRequestHeaders.Clear();
                if (apiResquest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiResquest.Data),
                        Encoding.UTF8, "application/json");
                }

                if (!string.IsNullOrEmpty(apiResquest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiResquest.AccessToken);
                }

                HttpResponseMessage apiResponse = null;

                switch (apiResquest.apiType)
                {
                    case Common.StaticDetails.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case Common.StaticDetails.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Common.StaticDetails.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();

                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                // var apiResponseDto = JsonConvert.DeserializeObject<List<T>>(apiContent);

                return apiResponseDto;

            }
            catch (Exception e)
            {

                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };

                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;

            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
