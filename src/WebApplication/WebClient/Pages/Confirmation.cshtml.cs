using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages
{
    public class ConfirmationModel : PageModel
    {
        public string Message { get; set; }

        public void OnGetContact()
        {
            Message = "Your email was sent successfully.";
        }

        public void OnGetOrderSubmitted()
        {
            Message = "Your order was submitted successfully.";
        }
    }
}
