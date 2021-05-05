using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Pages
{
    //This is the basket page with the necessary methods implemented
    public class CartModel : PageModel
    {
        //Injecting basket service.
        private readonly IBasketService basketService;

        //Assigning a basket object for the BasketModel
        public BasketModel Cart { get; set; } = new BasketModel();

        //Constructor.
        public CartModel(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        //This method is getting the basket with the given username.
        public async Task<IActionResult> OnGetAsync()
        {
            string userName = "rdu";
            Cart = await basketService.GetBasket(userName);

            return Page();
        }

        //This method removes items from the shopping basket.
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
