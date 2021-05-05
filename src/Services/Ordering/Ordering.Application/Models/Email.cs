namespace Ordering.Application.Models
{
    //This class represents the common features for sending an email.
    public class Email
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
