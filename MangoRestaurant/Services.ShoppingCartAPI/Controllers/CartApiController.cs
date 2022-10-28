using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ShoppingCartAPI.Messages;
using Services.ShoppingCartAPI.Models.Dto;
using Services.ShoppingCartAPI.Repository.Business;

namespace Services.ShoppingCartAPI.Controllers
{

    [Route("api/cart")]
    public class CartApiController : Controller
    {

        private readonly IcartRepository _cartRepository;
        protected ResponseDto _response;
     

        public CartApiController(IcartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            this._response = new ResponseDto();
           
        }



        [HttpGet("getCart/{userId}")]
        public async Task<object> Get(string userId)
        {
            try
            {

                CartDto cartDto = await _cartRepository.GetCartByUserId(userId);
                _response.Result = cartDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }


        [HttpPost("AddCart")]
        public async Task<object> AddCart([FromBody] CartDto cartDto)
        {
            try
            {

                CartDto cart = await _cartRepository.CreateUpdateCart(cartDto);
                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }




        [HttpPut("UpdateCart")]
        public async Task<object> UpdateCart(CartDto cartDto)
        {
            try
            {

                CartDto cart = await _cartRepository.CreateUpdateCart(cartDto);
                _response.Result = cart;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpDelete("RemoveCart")]
        public async Task<object> RemoveCart([FromBody] int cartId)
        {
            try
            {

                bool isSuccess = await _cartRepository.RemoveFromCart(cartId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }


        [HttpPost("ApplyCoupon")]
        public async Task<object> ApplyCoupon([FromBody] CartDto cartDto)
        {
            try
            {

                bool isSuccess = await _cartRepository.ApplyCoupon(cartDto.CartHeader.UserId, cartDto.CartHeader.CouponCode);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<object> RemoveCoupon([FromBody] string userId)
        {
            try
            {

                bool isSuccess = await _cartRepository.RemoveCoupon(userId);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost("Checkout")]
        public async Task<object> Checkout([FromBody] CartDto cartDto)
        {
            try
            {
                CartDto cartDtoObject = await _cartRepository.GetCartByUserId(cartDto.CartHeader.UserId);
                if (cartDtoObject == null)
                {
                    return BadRequest();
                }
                cartDtoObject.CartDetails = cartDto.CartDetails;                                       

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

    }
}
