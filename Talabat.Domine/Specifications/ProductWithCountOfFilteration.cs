using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Domine.Specifications
{
    public class ProductWithCountOfFilteration : SpecificationBase<Product>
    {
        public ProductWithCountOfFilteration(ProductSpecificationParameters parameters)
            : base(
                 P =>
                    (string.IsNullOrEmpty(parameters.Search) || P.Name.ToLower().Contains(parameters.Search)) &&
                    (!parameters.brandId.HasValue || P.ProductBrandId == parameters.brandId.Value) &&
                    (!parameters.typeId.HasValue || P.ProductTypeId == parameters.typeId.Value)
                )
        {

        }
    }
}
