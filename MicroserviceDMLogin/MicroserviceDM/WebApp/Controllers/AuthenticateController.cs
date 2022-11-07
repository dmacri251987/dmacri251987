using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        private readonly IConfiguration _configuration;

        public AuthenticateController(IGatewayService gatewayService, IIdentityService identityService, ILogger<HomeController> logger, IConfiguration configuration)
        {
            _gatewayService = gatewayService;
            _identityService = identityService;
            _logger = logger;
            _configuration = configuration;
        }


        #region ActionResult

        [HttpGet]
        public async Task<IActionResult> Index(LoginDto loginDto)
        {

            return View();

        }


        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            try
            {
                Token tokenClass = new Token();

                var loginValid = ValidateLogin(loginDto);

                if (!loginValid)
                {
                    TempData["LoginFailed"] = $"The username or password is incorrect.";

                    return Redirect("/login");
                }
                else
                {

                    var token = await _identityService.Login<string>(loginDto);

                    if (token != "")
                    {
                        await SignInUser(loginDto.UserName);

                    }
                    //Guardo Token en las cookies
                    var tokenResult = new Token().GeToken(token.ToString());
                    SetCookiesToken(tokenResult);


                    HttpContext.Session.SetString("tokenString", token);


                    //var tk = Request.Cookies["token"];

                    if (token != "")
                    {
                        return RedirectToAction("Index", controllerName: "Product");
                    }
                }

                return View();



            }
            catch (Exception ex)
            {
                _logger.LogError("Error detalle: {0}", ex.ToString());
                throw;
            }

        }

        #endregion




        #region Private
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Authenticate");
        }

        private bool ValidateLogin(LoginDto loginDto)
        {
            //Falta las validaciones
            return true;
        }

        //Claim que pide asp para poder authenticar
        private async Task SignInUser(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("authenticate_cookies", username)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
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

        #endregion


    }
}
