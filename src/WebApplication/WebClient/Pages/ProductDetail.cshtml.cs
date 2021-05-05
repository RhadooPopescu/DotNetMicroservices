using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Pages
{
    //This class contains the methods that can be accessed from the product details page of the application.
    public class ProductDetailModel : PageModel
    {
        //Injecting market and basket services.
        private readonly IMarketService marketService;
        private readonly IBasketService basketService;

        //Constructor.
        public ProductDetailModel(IMarketService marketService, IBasketService basketService)
        {
            this.marketService = marketService ?? throw new ArgumentNullException(nameof(marketService));
            this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public MarketModel Product { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        //Getting the requested product by productId.
        public async Task<IActionResult> OnGetAsync(string productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await marketService.GetMarket(productId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        //This method is adding products in the shopping basket from the market page.
        //Checking if the product allready exist in the basket by productId and incrementing the value of the product.
        public async Task<IActionResult> OnPostAddToCartAsync(string productId, int quantity)
        {
            MarketModel product = await marketService.GetMarket(productId);

            string userName = "rdu";
            BasketModel basket = await basketService.GetBasket(userName);

            try
            {
                BasketItemModel existingItem = basket.Items.Single(x => x.ProductId == productId);
                existingItem.Quantity += quantity;
            }
            catch (InvalidOperationException e)
            {
                basket.Items.Add(new BasketItemModel
                {
                    ProductId = productId,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = quantity,
                });
                Console.WriteLine(e.Message);
            }

            BasketModel basketUpdated = await basketService.UpdateBasket(basket);

            return RedirectToPage("Cart");
        }
    }
}
