using System.Collections.Generic;

namespace Shopping.Aggregator.Models
{
    //This class should include the same properties as the ShoppingBasket class.
    public class BasketModel
    {
        public string UserName { get; set; }
        public List<BasketItemExtendedModel> Items { get; set; } = new List<BasketItemExtendedModel>();
        public decimal TotalPrice { get; set; }
    }
}
