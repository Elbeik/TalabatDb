using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Domine.Specifications
{
    public class ProductTypeAndBarndSpec:SpecificationBase<Product>
    {
        
        public ProductTypeAndBarndSpec(ProductSpecificationParameters parameters)
            :base(
                 P => 
                    (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search)) &&
                    (!parameters.brandId.HasValue || P.ProductBrandId == parameters.brandId.Value) &&
                    (!parameters.typeId.HasValue || P.ProductTypeId == parameters.typeId.Value)   
                )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);


            ApplayPagination(parameters.PageSize * (parameters.pageIndex - 1), parameters.PageSize);

            if (true) //!string.IsNullOrEmpty(sort)
            {
                switch(parameters.sort)
                {
                    case "priceAsc":
                        AddOrderByAsc(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDes(P => P.Price);
                        break;
                    default:
                        AddOrderByAsc(P => P.Name);
                        break;
                        
                }
            }
            
        }

        public ProductTypeAndBarndSpec(int id):base(p => p.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
