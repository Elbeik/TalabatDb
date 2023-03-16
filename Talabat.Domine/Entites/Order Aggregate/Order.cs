using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Domine.Entites.Order_Aggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {

        }
        public Order(string buyerEmail, Address shippingAddress, ICollection<OrderItem> orderItems, decimal subTotal, DelivreyMethod delivreyMethod)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            SubTotal = subTotal;
            DelivreyMethod = delivreyMethod;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public DelivreyMethod DelivreyMethod { get; set; }
        public string PaymentIntentId { get; set; }

        public decimal GetTotal()
            => SubTotal + DelivreyMethod.Cost;
    }
}
