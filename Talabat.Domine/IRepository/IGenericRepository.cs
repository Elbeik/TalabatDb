using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Domine.Specifications;

namespace Talabat.Domine.IRepository
{
    public interface IGenericRepository<T>  where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> specification);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> specification);

        Task<int> GetCountAsync(ISpecification<T> specification);

        Task CreateAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
