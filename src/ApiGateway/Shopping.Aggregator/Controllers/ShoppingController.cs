using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly IMarketService marketService;
        private readonly IBasketService basketService;
        private readonly IOrderService orderService;

        public ShoppingController(IMarketService marketService, IBasketService basketService, IOrderService orderService)
        {
            this.marketService = marketService ?? throw new ArgumentNullException(nameof(marketService));
            this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }


        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName) 
        {
            //get basket with username
            //iterate basket items and cosume products with basket item productId member
            //map product related members into basketitem dto with extended columns
            //cosume orderin microservices in order to retrieve order list
            //return ShoppingModel dto class which includes all responses

            BasketModel basket = await basketService.GetBasket(userName);

            foreach (BasketItemExtendedModel item in basket.Items)
            {
                MarketModel product = await marketService.GetMarket(item.ProductId);

                // set additional product fields
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var orders = await orderService.GetOrdersByUserName(userName);

            ShoppingModel shoppingModel = new ShoppingModel
            {
                UserName = userName,
                BasketWithProducts = basket,
                Orders = orders
            };

            return Ok(shoppingModel);
        }
    }
}
