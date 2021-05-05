using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebClient.Models;
using WebClient.Services;


namespace WebClient.Pages
{
    //This is the order page which returns the completed orders.
    public class OrderModel : PageModel
    {
        //Injecting the order service.
        private readonly IOrderService orderService;

        //Constructor.
        public OrderModel(IOrderService orderService)
        {
            this.orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        //Assigning the order entity into the OrderResponseModel.
        public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();

        //This method returns the orders for a given username.
        public async Task<IActionResult> OnGetAsync()
        {
            Orders = await orderService.GetOrdersByUserName("rdu");

            return Page();
        }
    }
}
