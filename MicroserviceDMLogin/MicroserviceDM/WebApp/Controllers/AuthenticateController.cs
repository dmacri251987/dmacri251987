using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using WebApp.Common;
using WebApp.Models.DTOs.Identity;
using WebApp.Services.IServices;

namespace WebApp.Controllers
{
    public class AuthenticateController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IGatewayService _gatewayService;
        private readonly IIdentityService _identityService;

        public AuthenticateController(IGatewayService gatewayService, IIdentityService identityService, ILogger<HomeController> logger)
        {
            _gatewayService = gatewayService;
            _identityService = identityService;
            _logger = logger;
        }



        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            try
            {
                var token = await _identityService.Login<string>(loginDto);


                //Guardo Token en las cookies
                var tokenResult = GeToken(token.ToString());
                SetCookiesToken(tokenResult);



                //Guardo el refresh token en las cookies
                var refreshToken = GetRefreshToken();                      
                SetRefreshToken(refreshToken);



                HttpContext.Session.SetString("tokenString", token);


                //var tk = Request.Cookies["token"];

                if (token != "")
                {
                    return RedirectToAction("Index", controllerName: "Product");
                }

                return View();



            }
            catch (Exception ex)
            {
                _logger.LogError("Error detalle: {0}", ex.ToString());
                throw;
            }

        }



        private Token GeToken(string token)
        {
            var tokenResult = new Token
            {
                TokenString = token,
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now

            };

            return tokenResult;

        }

   

        private void SetCookiesToken(Token token)
        {

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = token.Expires
            };
            Response.Cookies.Append("tokenString", token.TokenString, cookieOptions);


        }

        private Token GetRefreshToken()
        {
            var refreshToken = new Token
            {
                TokenString = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now

            };

            return refreshToken;

        }

        private void SetRefreshToken(Token newRefreshToken)
        {

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.TokenString, cookieOptions);

        }

    }
}
