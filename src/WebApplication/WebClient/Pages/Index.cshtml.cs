using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMarketService _marketService;
        private readonly IBasketService _basketService;

        public IndexModel(IMarketService marketService, IBasketService basketService)
        {
            _marketService = marketService ?? throw new ArgumentNullException(nameof(marketService));
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public IEnumerable<MarketModel> ProductList { get; set; } = new List<MarketModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _marketService.GetMarket();
            return Page();
        }

        public async Task<IActionResult> OnPostAddToBasketAsync(string productId)
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
