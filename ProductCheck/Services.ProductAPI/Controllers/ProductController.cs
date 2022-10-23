using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.ProductAPI.Models.Dto;
using Services.ProductAPI.Repository.Business;

namespace Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        protected ResponseDto _response;


        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            this._response = new ResponseDto();
        }




        [HttpGet("GetProducts")]

        public async Task<ActionResult<ResponseDto>> GetProducts()
        {
         

            try
            {
                IEnumerable<ProductDto> productDto = new List<ProductDto>();
                productDto = await _productRepository.GetProductsAsync();             
                _response.Result = productDto;
            }
            catch (Exception ex)
            {
              
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
                productDtos = await _productRepository.GetProductByIdAsync(id);
                _response.Result= productDtos;
            }
            catch (Exception ex)            {

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
                productDtos = await _productRepository.CreateUpdateProductAsync(productDto);
                _response.Result = productDtos;
            }
            catch (Exception ex)
            {
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
                productDtos = await _productRepository.CreateUpdateProductAsync(productDto);
                _response.Result = productDtos;
            }
            catch (Exception ex)
            {
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
                productDtos = await _productRepository.GetProductByIdCategoryAsync(id);
                _response.Result = productDtos;
            }
            catch (Exception ex)
            {

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
                bool isSuccess = await _productRepository.DeleteProductAsync(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                //productDtos.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
