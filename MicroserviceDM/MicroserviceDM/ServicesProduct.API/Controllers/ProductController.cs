using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesCategory.Business.DTOs;
using ServicesProduct.Business.DTOs;
using ServicesProduct.Business.IService;

namespace ServicesProduct.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        protected ResponseDto _response;


        public ProductController(IProductService productService)
        {
            _productService = productService;
            this._response = new ResponseDto();
        }




        [HttpGet("GetProducts")]

        public async Task<ActionResult<ResponseDto>> GetProducts()
        {


            try
            {
                IEnumerable<ProductDto> productDto = new List<ProductDto>();
                productDto = await _productService.GetProductsAsync();
                _response.Result = productDto;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

                return BadRequest(_response);
            }
            return Ok(_response);


        }

        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<ResponseDto>> GetProductById(int id)
        {

            try
            {
                ProductDto productDtos = new ProductDto();
                productDtos = await _productService.GetProductByIdAsync(id);
                _response.Result = productDtos;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }
            return Ok(_response);
        }




        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ResponseDto>> Create([FromBody] ProductDto productDto)
        {

            try
            {
                ProductDto productDtos = new ProductDto();
                productDtos = await _productService.CreateUpdateProductAsync(productDto);
                _response.Result = productDtos;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }
            return Ok(_response);
        }





        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<ResponseDto>> Update([FromBody] ProductDto productDto)
        {

            try
            {
                ProductDto productDtos = new ProductDto();
                productDtos = await _productService.CreateUpdateProductAsync(productDto);
                _response.Result = productDtos;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }
            return Ok(_response);
        }


        [HttpGet("GetProductByIdCategory/{id}")]
        public async Task<ActionResult<ResponseDto>> GetProductByIdCategory(int id)
        {

            try
            {
                IEnumerable<ProductDto> productDtos = new List<ProductDto>();
                productDtos = await _productService.GetProductByIdCategoryAsync(id);
                _response.Result = productDtos;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }
            return Ok(_response);
        }


        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult<ResponseDto>> Delete(int id)
        {
            // ProductDto productDtos = new ProductDto();
            try
            {
                bool isSuccess = await _productService.DeleteProductAsync(id);
                _response.Result = isSuccess;
                _response.IsSuccess = isSuccess;
            }
            catch (Exception ex)
            {
                //productDtos.ErrorMessages = new List<string>() { ex.ToString() };
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
