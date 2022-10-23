﻿using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Mango.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;


        public CartController(IProductService productService, ICartService cartService, ICouponService couponService)
        {
            _productService = productService;
            _cartService = cartService;
            _couponService = couponService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await LoadCartDtoLoggedUser());
        }
     


        public async Task<IActionResult> Remove(int cartDetailsID)
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var acessToken = await HttpContext.GetTokenAsync("acess_token");
            var response = await _cartService.RemoveFromCartAsync<ResponseDto>(cartDetailsID, acessToken);


            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [ActionName("ApplyCoupon")]
        public async Task<IActionResult> ApplyCoupon(CartDto cartDto)
        {

            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var acessToken = await HttpContext.GetTokenAsync("acess_token");
            var response = await _cartService.ApplyCouponAsync<ResponseDto>(cartDto, acessToken);


            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [ActionName("RemoveCoupon")]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {

            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var acessToken = await HttpContext.GetTokenAsync("acess_token");
            var response = await _cartService.RemoveCouponAsync<ResponseDto>(cartDto.CartHeader.UserId, acessToken);


            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(Index));
            }

            return View();
        }



        public async Task<IActionResult> Checkout()
        {

            return View(await LoadCartDtoLoggedUser());
        }



        private async Task<CartDto> LoadCartDtoLoggedUser()
        {
            var userId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
            var acessToken = await HttpContext.GetTokenAsync("acess_token");
            var response = await _cartService.GetCartByUserIdAsync<ResponseDto>(userId, acessToken);

            CartDto cartDto = new();
            if (response != null && response.IsSuccess)
            {
                cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
            }

            if (cartDto.CartHeader != null)
            {
                if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
                {
                    var coupon = await _couponService.GetCoupon<ResponseDto>(cartDto.CartHeader.CouponCode, acessToken);
                    if (coupon != null && coupon.IsSuccess)
                    {
                        var couponObj = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(coupon.Result));
                        cartDto.CartHeader.DiscountTotal = couponObj.DiscountAmount;
                    
                    }
                }

                foreach (var detail in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
                }

                cartDto.CartHeader.OrderTotal -= cartDto.CartHeader.DiscountTotal;
            }

            return cartDto;

        }
    }
}