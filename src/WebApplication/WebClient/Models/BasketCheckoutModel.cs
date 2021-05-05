namespace WebClient.Models
{
    //This properties will be used when we send a post request to the basket microservices over the ocelot api gateway.
    public class BasketCheckoutModel
    {
        //Username and total price properties.
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        //Billing address properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        //Payment properties.
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}
