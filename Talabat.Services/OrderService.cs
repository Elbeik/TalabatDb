using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Domine.Entites.Order_Aggregate;
using Talabat.Domine.IRepository;
using Talabat.Domine.IServices;
using Talabat.Domine.Specifications;

namespace Talabat.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepositry _basketRepositry;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericRepository<Product> _productRepository;
        //private readonly IGenericRepository<DelivreyMethod> _deliveryRepository;
        //private readonly IGenericRepository<Order> _orderRepository;

        public OrderService(
            IBasketRepositry basketRepositry,
            IUnitOfWork unitOfWork
            //IGenericRepository<Product> productRepository,
            //IGenericRepository<DelivreyMethod> deliveryRepository,
            //IGenericRepository<Order> orderRepository

            )
        {
            _basketRepositry = basketRepositry;
            _unitOfWork = unitOfWork;
            //_productRepository = productRepository;
            //_deliveryRepository = deliveryRepository;
            //_orderRepository = orderRepository;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string baskedId, int delevirayMethode, Address shappingAddress)
        {
            // 1. Get Basket from Baskets repo
            var basked = await _basketRepositry.GetBasketAsync(baskedId);


            // 2. Get Selected items at Basket from Products Repo
            var orderItems  = new List<OrderItem>();

            foreach (var item in basked.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                
                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);

                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

                orderItems.Add(orderItem);
    
            }

            // 3. Calculate Subtotal
            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);


            // 4. Get Delivery Methode from DeliveryMethodes Repo
            var deliveryMethode = await _unitOfWork.Repository<DelivreyMethod>().GetByIdAsync(delevirayMethode);

            // 5. Create Order
            var order = new Order(buyerEmail, shappingAddress, orderItems,subTotal, deliveryMethode);

            await _unitOfWork.Repository<Order>().CreateAsync(order);

            var result = await _unitOfWork.Complete();
            if (result <= 0) return null;
            // 6. Save To Database [TODO]
            return order;
        }

        public async Task<IReadOnlyList<DelivreyMethod>> GetDelivreyMethodAsync()
        {
            var deliveyMethod = await _unitOfWork.Repository<DelivreyMethod>().GetAllAsync();
            return deliveyMethod;
        }

        public async Task<Order> GetOrderByIdUser(int orderId, string buyerEmail)
        {
            var spec = new OrderWithOtemsAndDeilveryMethodSpecification(orderId, buyerEmail);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return order;
        }

        public async Task<IReadOnlyList<Order>> GetUserOrderAsync(string buyerEmail)
        {
            var spec = new OrderWithOtemsAndDeilveryMethodSpecification(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
    }
}
