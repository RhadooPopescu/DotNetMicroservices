﻿namespace Shopping.Aggregator.Models
{
    //This class should include the same fields as the ShoppingBasketItem and the additional product related properties.
    public class BasketItemExtendedModel
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }

        //Product additional fields.
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
    }
}
