﻿namespace Shopping.Aggregator.Models
{
    //This class should iclude the same properties as the Order class in the Ordering.API
    public class OrderResponseModel
    {
        //Username and total price properties.
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }

        //Billing address properties.
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
