using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages
{
    //This is the confirmation page after completing an order.
    public class ConfirmationModel : PageModel
    {
        //Message property.
        public string Message { get; set; }

        //This method returns the following message.
        public void OnGetContact()
        {
            Message = "Your email was sent successfully.";
        }

        //This method returns the following message.
        public void OnGetOrderSubmitted()
        {
            Message = "Your order was submitted successfully.";
        }
    }
}
