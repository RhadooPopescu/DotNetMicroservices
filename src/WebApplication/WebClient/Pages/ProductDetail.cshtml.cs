using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Pages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IMarketService marketService;
        private readonly IBasketService basketService;

        public ProductDetailModel(IMarketService marketService, IBasketService basketService)
        {
            this.marketService = marketService ?? throw new ArgumentNullException(nameof(marketService));
            this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public MarketModel Product { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

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

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            MarketModel product = await marketService.GetMarket(productId);

            string userName = "rdu";
            BasketModel basket = await basketService.GetBasket(userName);

            basket.Items.Add(new BasketItemModel
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
            });

            BasketModel basketUpdated = await basketService.UpdateBasket(basket);

            return RedirectToPage("Cart");
        }
    }
}
