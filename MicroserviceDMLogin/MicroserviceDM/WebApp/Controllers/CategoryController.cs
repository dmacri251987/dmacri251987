using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Common;
using WebApp.Models.DTOs;
using WebApp.Models.DTOs.Category;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {

        private readonly IGatewayService _gatewayService;
        private readonly IIdentityService _identityService;
        private ICategoryService _categoryService;  

        public CategoryController(IGatewayService gatewayService, IIdentityService identityService, ICategoryService categoryService)
        {
            _gatewayService = gatewayService;
            _identityService = identityService;
            _categoryService = categoryService;
        }





        #region ActionResult

        public async Task<IActionResult> Index()
        {
            List<CategoryDto> list = new List<CategoryDto>();

            var token = HttpContext.Request.Cookies["tokenString"];
            token = await RefreshToken(token);

            if (token == null)
            {
                return NotFound();
            }

            var response = await _categoryService.GetCategoriesAsync<ResponseDto>(token);

            //Ejemplo de llamado del api Gateway            
            //var response3 = await _gatewayService.GetProductsCategories<CategoryDto>();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CategoryDto response = new CategoryDto();

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto CategoryDto)
        {

            CategoryDto response = new CategoryDto();

            var token = HttpContext.Request.Cookies["tokenString"];
            token = await RefreshToken(token);

            if (CategoryDto.CategoryID == 0 && token != null)
            {

                response = await _categoryService.CreateCategoryAsync<CategoryDto>(CategoryDto, token.ToString());

            }

            if (response != null && response.IsSuccess)
            {
                response = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
            }

            if (!response.IsSuccess)
            {
                return View(response);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(CategoryDto CategoryDto)
        {
            CategoryDto response = new CategoryDto();

            var token = HttpContext.Request.Cookies["tokenString"];
            token = await RefreshToken(token);


            if (CategoryDto.CategoryID > 0)
            {
                response = await _categoryService.GetCategoryByIdAsync<CategoryDto>(CategoryDto.CategoryID, token.ToString());
            }

            if (response != null && response.IsSuccess)
            {
                response = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
            }

            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> EditProduct(CategoryDto CategoryDto)
        {
            CategoryDto response = new CategoryDto();

            if (CategoryDto.CategoryID > 0)
            {
                response = await _categoryService.UpdateCategoryAsync<CategoryDto>(CategoryDto);
            }

            if (!response.IsSuccess)
            {
                return View(response);
            }

            if (response != null && response.IsSuccess)
            {
                response = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> GetDelete(int productId)
        {
            CategoryDto response = new CategoryDto();

            if (productId > 0)
            {
                var token = HttpContext.Request.Cookies["tokenString"];
                token = await RefreshToken(token);

                response = await _categoryService.GetCategoryByIdAsync<CategoryDto>(productId, token);
            }

            if (!response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            if (response != null && response.IsSuccess)
            {
                response = JsonConvert.DeserializeObject<CategoryDto>(Convert.ToString(response.Result));

            }

            return View(nameof(Delete), response);
        }


        public async Task<IActionResult> Delete(int productId)
        {
            CategoryDto response = new CategoryDto();

            if (productId > 0)
            {
                response = await _categoryService.DeleteCategoryAsync<CategoryDto>(productId);
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
