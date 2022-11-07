using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using WebApp.Common;
using WebApp.Models.DTOs;
using WebApp.Models.DTOs.Identity;
using WebApp.Models.DTOs.Product;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IGatewayService _gatewayService;
        private readonly IIdentityService _identityService;
     

        public ProductController(IProductService productService, IGatewayService gatewayService, IIdentityService identityService)
        {
            _productService = productService;
            _gatewayService = gatewayService;
            _identityService = identityService;          
        }


        #region ActionResult

        public async Task<IActionResult> Index()
        {
            List<ProductDto> list = new List<ProductDto>();

            var token = HttpContext.Request.Cookies["tokenString"];
            token = await RefreshToken(token);

            if (token == null)
            {
                return NotFound();
            }

            var response = await _productService.GetProductsAsync<ResponseDto>(token);

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

            var token = HttpContext.Request.Cookies["tokenString"];
            token = await RefreshToken(token);

            if (productDto.ProductID == 0 && token != null)
            {

                response = await _productService.CreateProductAsync<ProductDto>(productDto, token.ToString());

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

            var token = HttpContext.Request.Cookies["tokenString"];
            token = await RefreshToken(token);


            if (productDto.ProductID > 0)
            {
                response = await _productService.GetProductByIdAsync<ProductDto>(productDto.ProductID, token.ToString());
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
                var token = HttpContext.Request.Cookies["tokenString"];
                token = await RefreshToken(token);

                response = await _productService.GetProductByIdAsync<ProductDto>(productId, token);
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

        #endregion



        #region Private

        private async Task<string> RefreshToken(string token)
        {
            if (token == null || token == "")

            {
                token = await _identityService.RefreshTokenAsync<string>();
            }

            var tokenObj = new Token().GeToken(token.ToString());


            //Guardo Token en las cookies
            var cookieOptions = new Token().SetCookiesToken(tokenObj);

            if (cookieOptions != null)
            {
                Response.Cookies.Append("tokenString", token, cookieOptions);
            }


            Response.Cookies.Append("tokenString", token, cookieOptions);

            return token;
        }

        #endregion

    }
}

