﻿namespace Shopping.Aggregator.Models
{
    //This class should include the same properties as the Product class in the Market.API.
    public class MarketModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}
