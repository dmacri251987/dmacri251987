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

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new List<ProductDto>();

            var response = await _productService.GetProductsAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
    }
}
