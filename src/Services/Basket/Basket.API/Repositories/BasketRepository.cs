using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    //This class is going to perform withing the IBasketRepository with IDistributedCache.
    public class BasketRepository : IBasketRepository
    {
        //Injecting caching distribution.
        private readonly IDistributedCache redisCache;

        //Constructor.
        public BasketRepository(IDistributedCache redisChache)
        {
            this.redisCache = redisChache ?? throw new ArgumentNullException(nameof(redisChache));
        }

        //This method will retrieve the basket for a provided username and return the shopping basket.
        public async Task<ShoppingBasket> GetBasket(string userName)
        {
            //getting the username information.
            string basket = await redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(basket))
                return null;

            //using json converter in order to deserialize the string to the shopping basket object type.
            return JsonConvert.DeserializeObject<ShoppingBasket>(basket);
        }

        //This method updates the shopping basket for a given username.
        public async Task<ShoppingBasket> UpdateBasket(ShoppingBasket basket)
        {
            //getting the basket for the provided username.
            string basketUserName = basket.UserName;

            //
            await redisCache.SetStringAsync(basketUserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basketUserName);
        }

        //This method deletes the shopping basket for a given username.
        public async Task DeleteBasket(string userName)
        {
            await redisCache.RemoveAsync(userName);
        }
    }
}
