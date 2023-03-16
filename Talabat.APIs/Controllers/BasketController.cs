using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;
using Talabat.APIs.Dtos;
using Talabat.Domine.Entites;
using Talabat.Domine.IRepository;

namespace Talabat.APIs.Controllers
{

    public class BasketController : GenericController
    {
        public readonly IBasketRepositry _basketRepositry;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepositry basketRepositry, IMapper mapper)
        {
            _basketRepositry = basketRepositry;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepositry.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasketAsync(CustomerBaskeDto basketDto)
        {
            var mappBasked = _mapper.Map<CustomerBaskeDto, CustomerBasket>(basketDto);
            var resultBasket = await _basketRepositry.UpdateBaskedAsync(mappBasked);
            return Ok(resultBasket);
        }
        [HttpDelete("{id}")]
        public async Task DeleteBasket(string id)
        {
            await _basketRepositry.DeleteBasketAsync(id);
        }

    }
}
