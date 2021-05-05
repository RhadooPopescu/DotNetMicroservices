using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Pages
{
    //This class contains the methods that can be accessed from the home page of the application.
    public class IndexModel : PageModel
    {
        //Injecting market and basket services.
        private readonly IMarketService marketService;
        private readonly IBasketService basketService;

        //Getting the products from the market model in storing the in the ProductList.
        public IEnumerable<MarketModel> ProductList { get; set; } = new List<MarketModel>();


        //Constructor.
        public IndexModel(IMarketService marketService, IBasketService basketService)
        {
            this.marketService = marketService ?? throw new ArgumentNullException(nameof(marketService));
            this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        
        //Listing the products into the home page.
        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await marketService.GetMarket();
            return Page();
        }

        //This method is adding items into the shopping basket.
        //Checking if the product allready exist in the basket by productId and incrementing the value of the product.
        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            MarketModel product = await marketService.GetMarket(productId);

            string userName = "rdu";
            BasketModel basket = await basketService.GetBasket(userName);

            try
            {
                BasketItemModel existingItem = basket.Items.Single(x => x.ProductId == productId);
                existingItem.Quantity += 1;
            }
            catch(InvalidOperationException e)
            {
                basket.Items.Add(new BasketItemModel
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                });
                Console.WriteLine(e.Message);
            }
            BasketModel basketUpdated = await basketService.UpdateBasket(basket);
            return RedirectToPage("Cart");
        }
    }
}
