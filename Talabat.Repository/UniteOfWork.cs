using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Domine.IRepository;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UniteOfWork : IUnitOfWork
    {
        private Hashtable _repository;
        private readonly StoreContext context;

        public UniteOfWork(StoreContext context)
        {
            this.context = context;
        }
        public async Task<int> Complete()
            => await context.SaveChangesAsync();

        public void Dispose()
            => context.Dispose();

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if(_repository == null)
                _repository = new Hashtable();

            var type = typeof(TEntity).Name;
            if(!_repository.ContainsKey(type))
            {
                var repo = new GenericRepository<TEntity>(context);
                _repository.Add(type, repo);

            }

            return (IGenericRepository<TEntity>)_repository[type];
        }
    }
}
