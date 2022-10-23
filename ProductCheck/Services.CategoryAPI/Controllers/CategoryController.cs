using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.CategoryAPI.Models.Dto;
using Services.CategoryAPI.Repository.Business;

namespace Services.CategoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        protected ResponseDto _response;


        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            this._response = new ResponseDto();

        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult<ResponseDto>> GetCategories()
        {

           
            try
            {
                IEnumerable<CategoryDto> categoryDto = new List<CategoryDto>();
                categoryDto = await _categoryRepository.GetCategoriesAsync();
                _response.Result = categoryDto;
            }
            catch (Exception ex)
            {
             
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }

            return Ok(_response);
        }



        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<ResponseDto>> GetCategoryById(int id)
        {
          

            try
            {
                CategoryDto categoryDto = new CategoryDto();
                categoryDto = await _categoryRepository.GetCategoriesByIdAsync(id);
                _response.Result = categoryDto;
            }
            catch (Exception ex)
            {

                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpPost("CreateCategory")]
        public async Task<ActionResult<ResponseDto>> CreateCategory([FromBody] CategoryDto categoryDto)
        {
          

            try
            {
                categoryDto = await _categoryRepository.CreateUpdateCategoryAsync(categoryDto);
                _response.Result = categoryDto;

            }
            catch (Exception ex)
            {

                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<ResponseDto>> UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                categoryDto = await _categoryRepository.CreateUpdateCategoryAsync(categoryDto);
                _response.Result = categoryDto;

            }
            catch (Exception ex)
            {

                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }

            return Ok(_response);
        }

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteCategory(int id)
        {
            try
            {
                bool isSuccess = await _categoryRepository.DeteleCategoryAsync(id);
                _response.Result = isSuccess;

            }
            catch (Exception ex)
            {
                return BadRequest(_response);
            }
            return Ok(_response);
        }

    }
}
