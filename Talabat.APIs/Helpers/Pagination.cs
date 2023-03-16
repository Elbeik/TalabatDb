using System.Collections.Generic;
using Talabat.APIs.Dtos;

namespace Talabat.APIs.Helpers
{
    public class Pagination<T>
    {
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public int count { get; set; }
        public IEnumerable<T> Data { get; set; }

        public Pagination(int pageIndex, int pageSize, int count, IEnumerable<T> data)
        {
            this.pageIndex = pageIndex;
            this.pageSize = pageSize;
            this.Data = data;
            this.count = count;
    }

       
    }
}
