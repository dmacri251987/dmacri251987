using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesCategory.Business.DTOs;
using ServicesCategory.Business.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesCategory.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        protected ResponseDto _response;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _response = new ResponseDto();

        }

        #region ActionResult
        [HttpGet("GetCategories")]
        public async Task<ActionResult<ResponseDto>> GetCategories()
        {


            try
            {
                IEnumerable<CategoryDto> categoryDto = new List<CategoryDto>();
                categoryDto = await _categoryService.GetCategoriesAsync();
                _response.Result = categoryDto;
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



        [HttpGet("GetCategoryById/{id}")]
        public async Task<ActionResult<ResponseDto>> GetCategoryById(int id)
        {


            try
            {
                CategoryDto categoryDto = new CategoryDto();
                categoryDto = await _categoryService.GetCategoriesByIdAsync(id);
                _response.Result = categoryDto;
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

        [HttpPost("CreateCategory")]
        public async Task<ActionResult<ResponseDto>> CreateCategory([FromBody] CategoryDto categoryDto)
        {


            try
            {
                categoryDto = await _categoryService.CreateUpdateCategoryAsync(categoryDto);
                _response.Result = categoryDto;
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

        [HttpPut("UpdateCategory")]
        public async Task<ActionResult<ResponseDto>> UpdateCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                categoryDto = await _categoryService.CreateUpdateCategoryAsync(categoryDto);
                _response.Result = categoryDto;
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

        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteCategory(int id)
        {
            try
            {
                bool isSuccess = await _categoryService.DeteleCategoryAsync(id);
                _response.Result = isSuccess;
                _response.IsSuccess = isSuccess;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        #endregion
    }
}
