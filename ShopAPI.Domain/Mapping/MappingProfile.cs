using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Domain.Mapping
{
    using AutoMapper;
    using ShopAPI.Domain.DTO;
    using ShopAPI.Domain.Entities;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Маппинг для пользователя
            CreateMap<User, UserDto>();
            CreateMap<RegisterRequest, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserProfileDto>();

            // Маппинг для продукта
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // Маппинг для корзины
            CreateMap<CartItem, CartItemDto>();
            CreateMap<AddToCartDto, CartItem>();

            // Маппинг для заказа
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        }
    }

}
