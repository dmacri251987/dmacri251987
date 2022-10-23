﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.ShoppingCartAPI.DbContexts;
using Services.ShoppingCartAPI.Models;
using Services.ShoppingCartAPI.Models.Dto;
using Services.ShoppingCartAPI.Repository.Business;

namespace Services.ShoppingCartAPI.Repository.Data
{
    public class CartRepository : IcartRepository
    {

        private readonly ApplicationDbContext _db;
        private IMapper _mapper;

        public CartRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            var cartFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => userId == u.UserId);
            cartFromDb.CouponCode = couponCode;
            _db.CartHeaders.Update(cartFromDb);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCart(string userId)
        {
            var cartHeaderFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId);
            if (cartHeaderFromDb != null)
            {
                _db.CartDetails.RemoveRange(_db.CartDetails.Where(u => u.CartDetailsId == cartHeaderFromDb.CartHeaderId));
                _db.CartHeaders.Remove(cartHeaderFromDb);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
        {

            Cart cart = _mapper.Map<Cart>(cartDto);

            var prodInDb = await _db.Products.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == cartDto.CartDetails.FirstOrDefault().ProductId);
            if (prodInDb == null)
            {
                cart.CartDetails.FirstOrDefault().Product.ProductId = 0;
                _db.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _db.SaveChangesAsync();
            }


            var cartHeaderFromDb = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);

            if (cartHeaderFromDb == null)
            {
                //create header and details
                _db.CartHeaders.Add(cart.CartHeader);
                await _db.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.CartHeaderId;
                cart.CartDetails.FirstOrDefault().Product = null;
                cart.CartDetails.FirstOrDefault().ProductId = cartDto.CartDetails.FirstOrDefault().ProductId;
                _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _db.SaveChangesAsync();
            }
            else
            {
                //if header is not null
                //check if details has same product

                var cartDetailsFromDb = await _db.CartDetails.AsNoTracking().FirstOrDefaultAsync(u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId
                && u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                if (cartDetailsFromDb == null)
                {
                    //create details
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().ProductId = cart.CartDetails.FirstOrDefault().ProductId;
                    _db.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
                else
                {
                    //update the count / cart details
                    //cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    cart.CartDetails.FirstOrDefault().ProductId = prodInDb.ProductId;
                    _db.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _db.SaveChangesAsync();
                }
            }


            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> GetCartByUserId(string userId)
        {
            Cart cart = new()
            {
                CartHeader = await _db.CartHeaders.FirstOrDefaultAsync(u => u.UserId == userId)
            };

            cart.CartDetails = _db.CartDetails.Where(u => u.CartHeaderId == cart.CartHeader.CartHeaderId).Include(u => u.Product);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> RemoveCoupon(string userId)
        {
            var cartFromDb = await _db.CartHeaders.FirstOrDefaultAsync(u => userId == u.UserId);
            cartFromDb.CouponCode = "";
            _db.CartHeaders.Update(cartFromDb);
            _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromCart(int cartDetailsId)
        {

            try
            {
                CartDetails cartDetails = await _db.CartDetails.FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);

                int totalCounofItems = _db.CartDetails.Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();
                _db.CartDetails.Remove(cartDetails);
                if (totalCounofItems == 1)
                {
                    var cartHeaderToRemove = await _db.CartHeaders.FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                    _db.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                return false;

            }
        }
    }
}