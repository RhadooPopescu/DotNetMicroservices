using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Pages
{
    public class CartModel : PageModel
    {
        private readonly IBasketService basketService;

        public CartModel(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            string userName = "rdu";
            Cart = await basketService.GetBasket(userName);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            string userName = "rdu";
            BasketModel basket = await basketService.GetBasket(userName);

            BasketItemModel item = basket.Items.Single(x => x.ProductId == productId);
            basket.Items.Remove(item);

            BasketModel basketUpdated = await basketService.UpdateBasket(basket);

            return RedirectToPage();
        }
    }
}
