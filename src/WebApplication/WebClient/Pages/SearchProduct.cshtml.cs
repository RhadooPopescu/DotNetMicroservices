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
    //This class contains the methods that can be accessed from the search function of the application.
    public class SearchProductModel : PageModel
    {
        //Injecting market and basket service.
        private readonly IMarketService marketService;
        private readonly IBasketService basketService;
        
        //Constructor.
        public SearchProductModel(IMarketService marketService, IBasketService basketService)
        {
            this.marketService = marketService ?? throw new ArgumentNullException(nameof(marketService));
            this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public IEnumerable<MarketModel> ProductList { get; set; } = new List<MarketModel>();

        //This method searches for products by name or category.
        public async Task<IActionResult> OnGetAsync(string searchKey)
        {
            
            ProductList = await marketService.GetMarket();
            if (!string.IsNullOrWhiteSpace(searchKey))
            {
                List<MarketModel> filteredProductList = new List<MarketModel>();
                foreach (var product in ProductList)
                {
                    if (product.Name.ToLower().Contains((searchKey).ToLower()) || product.Category.ToLower().Contains((searchKey).ToLower()))
                    {
                        filteredProductList.Add(product);
                    }
                }
                ProductList = filteredProductList;
            }
            return Page();
        }

        //This method is adding products in the shopping basket from the market page.
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
            catch (InvalidOperationException e)
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
