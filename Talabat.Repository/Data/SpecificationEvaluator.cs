using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Domine.Specifications;

namespace Talabat.Repository.Data
{
    internal class SpecificationEvaluator<T> where T : BaseEntity
    {
       public static IQueryable<T> GetQuery(IQueryable<T> inputPoint, ISpecification<T> spec)
        {
            var query = inputPoint; // _context.Set<product>()
            if(spec.Criteria != null)
                query = query.Where(spec.Criteria); // _context.Set<product>().Where(P => P.Id == 1)

            if(spec.isPagination)
                query = query.Skip(spec.Skip).Take(spec.Take);
                

            if(spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            if(spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            query = spec.Includes.Aggregate(query, (standertQuery, addedQuery) => standertQuery.Include(addedQuery));

            return query;





        }
    }
}
