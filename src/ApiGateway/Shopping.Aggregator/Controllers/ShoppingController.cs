using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    //This class will contain a GetShopping method which will consume our internal microservices by using our Service classes.
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        //Injecting the Service classes.
        private readonly IMarketService marketService;
        private readonly IBasketService basketService;
        private readonly IOrderService orderService;

        //Constructor.
        public ShoppingController(IMarketService marketService, IBasketService basketService, IOrderService orderService)
        {
            this.marketService = marketService ?? throw new ArgumentNullException(nameof(marketService));
            this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        //This method will consume our internal microservices by using our Service classes and returning our ShoppingModel.
        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName) 
        {
            //get basket with username.
            BasketModel basket = await basketService.GetBasket(userName);
            List<BasketItemExtendedModel> basketItems = basket.Items;

            //iterate basket items and cosume products with basket item productId member.
            foreach (BasketItemExtendedModel item in basketItems)
            {
                string currentProductId = item.ProductId;
                MarketModel product = await marketService.GetMarket(currentProductId);

                // set additional product fields.
                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }
            //consume ordering microservices for retrieving order list.
            var orders = await orderService.GetOrdersByUserName(userName);

            //return ShoppingModel dto class which includes all responses.
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
