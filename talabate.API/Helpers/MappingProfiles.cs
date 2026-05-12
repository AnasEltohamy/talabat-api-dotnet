using AutoMapper;
using talabat.API.DTOs.BasketAndOrderDtos;
using talabat.Core.Entites.Order_Aggregate;
using talabat.Core.Entites.Products;

using AddressOfUser = talabat.Core.Entites.Identity.Address;
using AddressOfOrder = talabat.Core.Entites.Order_Aggregate.Address;

namespace talabat.API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDTO>()
                .ForMember(D=>D.Brand , O=>O.MapFrom(S=>S.Brand.Name))
                .ForMember(D=>D.Category ,O=>O.MapFrom(S=>S.Category.Name))
                .ForMember(D=>D.PictureUrl,O=>O.MapFrom<ProductPictureUrlResolver>());

            //Mapping AddressDto To Address Of Order
            CreateMap<AddressDto, AddressOfOrder>();

            CreateMap<Order,OrderToReturnDTO>()
                .ForMember(D=>D.DeliveryMethod ,O=>O.MapFrom(S=>S.DeliveryMethod.ShortName))
                .ForMember(D=>D.DeliveryMethodCost,O=>O.MapFrom(S=>S.DeliveryMethod.Cost));

            CreateMap<OrderItem,OrderItemDTO>()
                .ForMember(D=>D.ProductId,O=>O.MapFrom(S=>S.Product.ProductId))
                .ForMember(D=>D.Name,O=>O.MapFrom(S=>S.Product.ProductName))
                .ForMember(D=>D.PictureUrl , O=>O.MapFrom<OrderPictureUrlResolver>());
            CreateMap<CustomerBasketDTO, CustomerBasket>().ReverseMap();

            CreateMap<BasketItemDto, BasketItem>();
            //CreateMap<BasketItem, BasketItemDto>();
            CreateMap< BasketItem, BasketItemDto>().ForMember(D => D.PictureUrl, O =>O.MapFrom<BasketPictureUrlResolver>());
               

          

            //Mapping AddressDto To Address Of Identity
            CreateMap<AddressOfUser, AddressDto>().ReverseMap();
        }
    }
}
