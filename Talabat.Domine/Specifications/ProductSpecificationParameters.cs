using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Domine.Specifications
{
    public class ProductSpecificationParameters
    {
        private const int pageMax = 7;
        public int pageIndex { get; set; } = 1;

        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > pageMax ? pageMax :  value; }
        }

        private string search;

        public string Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }


        public string sort { get; set; }
        public int? brandId { get; set; }
        public int? typeId { get; set; }
    }
}
