using static WebApp.Common.StaticDetails;

namespace WebApp.Models
{
    public class ApiResquest
    {
        public ApiType apiType { get; set; } = ApiType.GET;
        public string url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
