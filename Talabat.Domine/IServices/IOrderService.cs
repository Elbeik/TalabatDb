using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domine.Entites.Order_Aggregate;

namespace Talabat.Domine.IServices
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, string baskedId,int delevirayMethode, Address shappingAddress);

        Task<IReadOnlyList<Order>> GetUserOrderAsync(string buyerEmail);
        
        Task<Order> GetOrderByIdUser(int orderId, string buyerEmail);

        Task<IReadOnlyList<DelivreyMethod>> GetDelivreyMethodAsync();



    }
}
