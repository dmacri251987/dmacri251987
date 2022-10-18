﻿using Services.ShoppingCartAPI.Models.Dto;

namespace Services.ShoppingCartAPI.Repository.Business
{
    public interface IcartRepository
    {
        Task<CartDto> GetCartByUserId(string userId);
        Task<CartDto> CreateUpdateCart(CartDto cartDto);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> ClearCart(string userId);
    }
}