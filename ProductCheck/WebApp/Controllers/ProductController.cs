using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Models.Dto;
using WebApp.Models.Dto.Product;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IGatewayService _gatewayService;
        public ProductController(IProductService productService,IGatewayService  gatewayService)
        {
            _productService = productService;
            _gatewayService = gatewayService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new List<ProductDto>();

            var response = await _productService.GetProductsAsync<ResponseDto>();

            //Ejemplo de llamado del api Gateway            
            //var response3 = await _gatewayService.GetProductsCategories<ProductDto>();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
    }
}
