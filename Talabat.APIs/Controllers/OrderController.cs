using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.Domine.Entites.Order_Aggregate;
using Talabat.Domine.IServices;

namespace Talabat.APIs.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : GenericController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(
            IOrderService orderService,
            IMapper mapper
            )
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]

        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var addressMapper =  _mapper.Map<AddressDto, Address>(orderDto.ShappingAddress);


            var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.BaskedId, orderDto.DelivertMethode, addressMapper);

            if (order == null) return BadRequest();

            return Ok(order);

        }

        [HttpGet]
        public async Task<ActionResult<Order>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var userOrder = await _orderService.GetUserOrderAsync(buyerEmail);

            return Ok(userOrder);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Order>> GetOrderByIdForUser(int orderId)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var userIdOrder = await _orderService.GetOrderByIdUser(orderId, buyerEmail);
            if (userIdOrder == null) return BadRequest(new ApiResponse(400));

            return Ok(userIdOrder);
        }

        [HttpGet("{DeliveryMethod}")]

        public async Task<ActionResult<IReadOnlyList<DelivreyMethod>>> GetDeliveryMethods()
        {
            var deliveryMethod = await _orderService.GetDelivreyMethodAsync();
            return Ok(deliveryMethod);
        }


    }
}
