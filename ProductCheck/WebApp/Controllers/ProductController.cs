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
        public ProductController(IProductService productService, IGatewayService gatewayService)
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


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ProductDto response = new ProductDto();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            ProductDto response = new ProductDto();

            if (productDto.ProductID == 0)
            {

                response = await _productService.CreateProductAsync<ProductDto>(productDto);

            }

            if (response != null && response.IsSuccess)
            {
                response = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }

            if (!response.IsSuccess)
            {
                return View(response);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(ProductDto productDto)
        {
            ProductDto response = new ProductDto();

            if (productDto.ProductID > 0)
            {
                response = await _productService.GetProductByIdAsync<ProductDto>(productDto.ProductID);
            }

            if (response != null && response.IsSuccess)
            {
                response = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }

            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductDto productDto)
        {
            ProductDto response = new ProductDto();

            if (productDto.ProductID > 0)
            {
                response = await _productService.UpdateProductAsync<ProductDto>(productDto);
            }

            if (!response.IsSuccess)
            {
                return View(response);
            }

            if (response != null && response.IsSuccess)
            {
                response = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> GetDelete(int productId)
        {
            ProductDto response = new ProductDto();

            if (productId > 0)
            {
                response = await _productService.GetProductByIdAsync<ProductDto>(productId);
            }

            if (!response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            if (response != null && response.IsSuccess)
            {
                response = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

            }

            return View(nameof(Delete), response);
        }


        public async Task<IActionResult> Delete(int productId)
        {
            ProductDto response = new ProductDto();

            if (productId > 0)
            {
                response = await _productService.DeleteProductAsync<ProductDto>(productId);
            }

            if (!response.IsSuccess)
            {
                return View(response);
            }

            return RedirectToAction(nameof(Index));

        }


    }
}
