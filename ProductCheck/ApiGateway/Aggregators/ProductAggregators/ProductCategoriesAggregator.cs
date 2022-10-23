using ApiGateway.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Linq;
using System.Net.Http.Headers;

namespace ApiGateway.Aggregators.ProductAggregators
{
    public class ProductCategoriesAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {
            ResponseDto _response = new ResponseDto();
            try
            {

                var productsResponseContent = await responses[0].Items.DownstreamResponse().Content.ReadAsStringAsync();
                var categoriesResponseContent = await responses[1].Items.DownstreamResponse().Content.ReadAsStringAsync();


                var products = JsonConvert.DeserializeObject<ResponseDto>(productsResponseContent);
                var categories = JsonConvert.DeserializeObject<ResponseDto>(categoriesResponseContent);


                var objProduct = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(products.Result)).ToList();
                var objCategory = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(categories.Result)).ToList();

                objProduct.ToList().ForEach(prod =>
                {
                    prod.Categories.AddRange(objCategory.Where(a => a.CategoryID == prod.CategoryID));
                });


                _response.Result = objProduct;
                _response.IsSuccess = true;


                var jsonString = JsonConvert.SerializeObject(_response, Formatting.Indented, new JsonConverter[] { new StringEnumConverter() });

                var stringContent = new StringContent(jsonString)
                {
                    Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
                };
                return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string> { ex.ToString() };
                throw;
            }



        }
    }
}
