using AutoMapper;
using Microsoft.Extensions.Configuration;
using Talabat.APIs.Dtos;
using Talabat.Core.Entites;

namespace Talabat.APIs.Helpers
{
    public class ProductPictureUrlResorver : IValueResolver<Product, ProductToReturnDto, string>
    {
        public IConfiguration Configuration { get; }
        public ProductPictureUrlResorver(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{Configuration["BaseUrl"]}{source.PictureUrl}";

            return null;
        }
    }
}
