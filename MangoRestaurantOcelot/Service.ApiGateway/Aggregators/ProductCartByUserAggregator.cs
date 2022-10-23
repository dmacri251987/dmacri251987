using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using Service.ApiGateway.Dto.Products;
using Services.ShoppingCartAPI.Models.Dto;
using System.Net;
using System.Net.Http.Headers;

namespace Service.ApiGateway.Aggregators
{

    public class ProductCartByUserAggregator : IDefinedAggregator
    {
        public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
        {

            var products = await responses[0].Items.DownstreamResponse().Content.ReadFromJsonAsync<List<ProductDto>>();
            var cartUser = await responses[1].Items.DownstreamResponse().Content.ReadFromJsonAsync<List<CartDetailsDto>>();



            cartUser?.ForEach(cart =>
                {
                    cart.Product.AddRange(products?.Where(a => a.ProductId == cart.ProductId));
                });


            var jsonString = JsonConvert.SerializeObject(cartUser, Formatting.Indented, new JsonConverter[] { new StringEnumConverter() });

            var stringContent = new StringContent(jsonString)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
            };
            return new DownstreamResponse(stringContent, System.Net.HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");


        }
    }

}
