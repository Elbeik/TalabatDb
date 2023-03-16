using Microsoft.VisualBasic;
//using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Domine.Entites;
using Talabat.Domine.IRepository;

namespace Talabat.Repository
{
    public class BasketRepository : IBasketRepositry
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string baskedId)
        {
            var basket = await _database.StringGetAsync(baskedId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket); //JsonConvert.DeserializeObject<CustomerBasket>(basket);

        }

        public async Task<CustomerBasket> UpdateBaskedAsync(CustomerBasket basket)
        {
            var resultBasket = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (resultBasket == false) return null;
            return await GetBasketAsync(basket.Id);
        }
    }
}
