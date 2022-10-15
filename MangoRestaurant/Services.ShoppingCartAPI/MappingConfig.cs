using AutoMapper;
using Services.ShoppingCartAPI.Models;
using Services.ShoppingCartAPI.Models.Dto;

namespace Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>().ReverseMap();
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
                config.CreateMap<Cart, CartDto>().ReverseMap();

                //config.CreateMap<ProductDto, Product>();
                //config.CreateMap<Product, ProductDto>();
                //config.CreateMap<CartHeaderDto, CartHeader>();
                //config.CreateMap<CartHeader, CartHeaderDto>();
                //config.CreateMap<CartDetailsDto, CartDetails>();
                //config.CreateMap<CartDetails, CartDetailsDto>();
                //config.CreateMap<CartDto, Cart>();
                //config.CreateMap<Cart, CartDto>();

            });

            return mappingConfig;
        }
    }
}
