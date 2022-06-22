using AutoMapper;
using Backend.Dtos;
using Backend.Entities;

namespace Backend.Helpers
{
     /////46. Adding a Custom Value Resolver for AutoMapper
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl)){
                //set in appsetting.Development.json
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}