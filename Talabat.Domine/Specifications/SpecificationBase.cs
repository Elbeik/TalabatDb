using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Domine.Specifications
{
    public class SpecificationBase<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get; set ; }
        public Expression<Func<T, object>> OrderByDescending { get ; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool isPagination { get; set; }

        public SpecificationBase()
        {
            // its implement Includes just
        }

        public SpecificationBase(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public void AddOrderByAsc(Expression<Func<T, object>> orderByAsc)
        {
            OrderBy = orderByAsc;
        }

        public void AddOrderByDes(Expression<Func<T, object>> orderByDec)
        {
            OrderByDescending = orderByDec;
        }

        public void ApplayPagination(int skip, int take)
        {
            isPagination = true;
            Take = take;
            Skip = skip;
        }
    }
}
