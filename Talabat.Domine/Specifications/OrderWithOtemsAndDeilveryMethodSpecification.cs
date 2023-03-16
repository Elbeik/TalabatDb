using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domine.Entites.Order_Aggregate;

namespace Talabat.Domine.Specifications
{
    public class OrderWithOtemsAndDeilveryMethodSpecification : SpecificationBase<Order>
    {
        public OrderWithOtemsAndDeilveryMethodSpecification(string BuyerEmail)
            :base(O => O.BuyerEmail == BuyerEmail)
        {
            Includes.Add(O => O.DelivreyMethod);
            Includes.Add(O => O.OrderItems);

            AddOrderByDes(O => O.OrderDate);
        }

        public OrderWithOtemsAndDeilveryMethodSpecification(int orderId,string BuyerEmail)
            : base(O => O.BuyerEmail == BuyerEmail && O.Id == orderId)
        {
            Includes.Add(O => O.DelivreyMethod);
            Includes.Add(O => O.OrderItems);

        }
    }
}
