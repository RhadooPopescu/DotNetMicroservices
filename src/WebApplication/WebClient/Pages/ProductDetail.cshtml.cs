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
    public class ProductDetailModel : PageModel
    {
        private readonly IMarketService _marketService;
        private readonly IBasketService _basketService;

        public ProductDetailModel(IMarketService marketService, IBasketService basketService)
        {
            _marketService = marketService ?? throw new ArgumentNullException(nameof(marketService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
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

            Product = await _marketService.GetMarket(productId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            var product = await _marketService.GetMarket(productId);

            var userName = "rdu";
            var basket = await _basketService.GetBasket(userName);

            basket.Items.Add(new BasketItemModel
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1,
            });

            var basketUpdated = await _basketService.UpdateBasket(basket);

            return RedirectToPage("Cart");
        }
    }
}
