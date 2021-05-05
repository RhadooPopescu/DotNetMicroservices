namespace Ordering.Application.Models
{
    //This class handles the configuration from ASP.NET sendgrid implementation.
    public class EmailSettings
    {
        public string ApiKey { get; set; }
        public string FromAddress { get; set; }
        public string FromName { get; set; }

    }
}
