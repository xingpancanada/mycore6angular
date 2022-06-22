using AutoMapper;
using Backend.Dtos;
using Backend.Entities;

namespace Backend.Helpers
{
    public class MappingProfiles : Profile
    {
         public MappingProfiles()
        {
            /////44. Adding AutoMapper to the API project
            /////45. Configuring AutoMapper profile: Product.ProductBrand.Name  -> ProductToReturnDto.ProductBrand
            ////d: destination     o: source from
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                /////46. Adding a Custom Value Resolver for AutoMapper
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}