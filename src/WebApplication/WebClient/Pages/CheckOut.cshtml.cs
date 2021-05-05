using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Models;
using WebClient.Services;

namespace WebClient.Pages
{
    //This is our checkout page with the necessary methods implemented.
    public class CheckOutModel : PageModel
    {
        //Injecting basket and order services.
        private readonly IBasketService basketService;
        private readonly IOrderService orderService;

        //Constructor.
        public CheckOutModel(IBasketService basketService, IOrderService orderService)
        {
            this.basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        //Binding the order with the BasketCheckoutModel.
        [BindProperty]
        public BasketCheckoutModel Order { get; set; }

        //Assigning a basket object for the BasketModel
        public BasketModel Cart { get; set; } = new BasketModel();

        //This method is getting the basket with the given username.
        public async Task<IActionResult> OnGetAsync()
        {
            string userName = "rdu";
            Cart = await basketService.GetBasket(userName);

            return Page();
        }

        //This method handles the check out operation.
        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            string userName = "rdu";
            Cart = await basketService.GetBasket(userName);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.UserName = userName;
            Order.TotalPrice = Cart.TotalPrice;

            await basketService.CheckoutBasket(Order);

            return RedirectToPage("Confirmation", "OrderSubmitted");
        }
    }
}
