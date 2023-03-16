using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domine.Entites;

namespace Talabat.Domine.IRepository
{
    public interface IBasketRepositry
    {
        Task<CustomerBasket> GetBasketAsync(string baskedId);
        Task<CustomerBasket> UpdateBaskedAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
