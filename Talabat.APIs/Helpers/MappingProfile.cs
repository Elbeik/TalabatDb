using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entites;
using Talabat.Domine.Entites;
using Talabat.Domine.Entites.Identity;

namespace Talabat.APIs.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(p => p.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(p => p.ProductType, o => o.MapFrom(S => S.ProductType.Name))
                .ForMember(p => p.PictureUrl, O => O.MapFrom<ProductPictureUrlResorver>());

            CreateMap<AddressDto, Domine.Entites.Identity.Address>().ReverseMap();

            CreateMap<CustomerBaskeDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();

            CreateMap<AddressDto, Domine.Entites.Order_Aggregate.Address>();
        }
    }
}
