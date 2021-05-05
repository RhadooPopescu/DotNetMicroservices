using System.Collections.Generic;

namespace WebClient.Models
{
    //This properties will be used when we consume the basket microservices over the ocelot api gateway.
    public class BasketModel
    {
        //Properties.
        public string UserName { get; set; }
        public List<BasketItemModel> Items { get; set; } = new List<BasketItemModel>();
        public decimal TotalPrice { get; set; }
    }
}
