namespace WebApp.Common
{
    public class StaticDetails
    {
      
        public static string ProductAPIBase { get; set; }
        public static string CategoryAPIBase { get; set; }
     
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
