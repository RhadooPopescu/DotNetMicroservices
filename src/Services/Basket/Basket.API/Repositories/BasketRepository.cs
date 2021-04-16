using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache redisCache;

        public BasketRepository(IDistributedCache redisChache)
        {
            this.redisCache = redisChache ?? throw new ArgumentNullException(nameof(redisChache));
        }

        public async Task<ShoppingBasket> GetBasket(string userName)
        {
            string basket = await redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingBasket>(basket);
        }

        public async Task<ShoppingBasket> UpdateBasket(ShoppingBasket basket)
        {
            await redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await redisCache.RemoveAsync(userName);
        }
    }
}
