using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceClient.Business.DTOs;
using ServiceClient.Business.IService;

namespace ServicesIdentity.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        protected ResponseDto _response;

        public UserController(IUserService userService)
        {
            _userService = userService;
            this._response = new ResponseDto();
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<ResponseDto>> GetUsers()
        {


            try
            {
                IEnumerable<UserDto> userDto = new List<UserDto>();
                userDto = await _userService.GetUsersAsync();
                _response.Result = userDto;
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



        [HttpGet("GetUserById/{id}")]
        public async Task<ActionResult<ResponseDto>> GetCategoryById(int id)
        {


            try
            {
               UserDto userDto = new UserDto();
                userDto = await _userService.GetUserByIdAsync(id);
                _response.Result = userDto;
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
        public async Task<ActionResult<ResponseDto>> CreateCategory([FromBody] UserDto userDto)
        {


            try
            {
                userDto = await _userService.CreateUpdateUserAsync(userDto);
                _response.Result = userDto;
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
        public async Task<ActionResult<ResponseDto>> UpdateCategory([FromBody] UserDto userDto)
        {
            try
            {
                userDto = await _userService.CreateUpdateUserAsync(userDto);
                _response.Result = userDto;
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
                bool isSuccess = await _userService.DeteleUserAsync(id);
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


    }
}
