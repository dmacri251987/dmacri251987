using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ServiceClient.Business.DTOs;
using ServiceClient.Business.IService;
using ServicesCommon;
using ServicesIdentity.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ServicesIdentity.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {

        public static UserDto userDto = new UserDto();
        private IConfiguration _configuration;
        private readonly IUserService _userService;
        protected ResponseDto _response;


        public AuthenticateController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            this._response = new ResponseDto();
            _configuration = configuration;
        }


        #region ActionResult
        [HttpPost("Login")]

        public async Task<ActionResult<string>> Login([FromBody] LoginDto loginDto)
        {
            ActionResult<string> token = null;


            try
            {

                if (loginDto.UserName == null && loginDto.Password == null)
                {
                    return BadRequest("Campos invalidos");

                }

                var userObj = await _userService.GetUsersAsync();
                userDto = userObj.Where(u => u.UserName == loginDto.UserName && u.Password == loginDto.Password).FirstOrDefault();


                token = CreateToken();


            }
            catch (Exception ex)
            {
                return BadRequest("Error login");
            }


            return Ok(token.Value);

        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {

            string token = CreateToken();      

            return Ok(token);
        }


        [HttpPost("GenerateToken")]
        public async Task<ActionResult<string>> GenerateToken([FromBody] UserDto userDto)
        {
            try
            {

                if (userDto == null)
                {
                    _response.IsSuccess = false;
                    _response.Result = "";
                    _response.DisplayMessage = "Usuario no encontrado";
                }


                var jwt = _configuration.GetSection("Jwt").Get<JwtDto>();

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("Id", userDto.Id.ToString()),
                new Claim("User",userDto.UserName),
                new Claim("Email", userDto.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.KeyPrivate));
                var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, expires: DateTime.Now.AddMinutes(120));
                var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: singIn);
                


                if (token != null)
                {
                    _response.IsSuccess = true;
                    _response.Result = new JwtSecurityTokenHandler().WriteToken(token);
                    _response.DisplayMessage = "Token generado";
                }


            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response.ErrorMessages.ToString());
            }


            return _response.Result.ToString();

        }




        [HttpPost("ValidToken")]
        public async Task<ActionResult<ResponseDto>> ValidToken(ClaimsIdentity identity)
        {
            try
            {

                if (identity.Claims.Count() == 0)
                {

                    _response.IsSuccess = false;
                    _response.DisplayMessage = "Token invalido";
                    _response.Result = "";
                    return _response;
                }

                //Busco por el Id que se definio a la hora de crear los claim (metodo GenerarToken)
                var id = identity.Claims.FirstOrDefault(u => u.Type == "Id").Value;

                UserDto user = await _userService.GetUserByIdAsync(Convert.ToInt32(id));

                if (user.Id > 0)
                {
                    _response.IsSuccess = true;
                    _response.Result = user;
                    _response.DisplayMessage = "Usuario encontrado";
                }



            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest(_response);
            }



            return _response;

        }



        [HttpPost("CreateToken")]
        private string CreateToken()
        {

            string tokenResult = "";

            if (userDto == null)
            {
                _response.IsSuccess = false;
                _response.Result = "";
                _response.DisplayMessage = "Usuario no encontrado";
            }


            var jwt = _configuration.GetSection("Jwt").Get<JwtDto>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("Id", userDto.Id.ToString()),
                new Claim("User",userDto.UserName),
                new Claim("Email", userDto.Email)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.KeyPrivate));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, expires: DateTime.Now.AddMinutes(120));
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: singIn);


            if (token != null)
            {
                tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
            }


            return tokenResult;

        }


        #endregion
    }
}

