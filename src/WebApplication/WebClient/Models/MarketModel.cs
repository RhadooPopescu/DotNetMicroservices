namespace WebClient.Models
{
    //This properties will be used when we consume the market microservices over the ocelot api gateway.
    public class MarketModel
    {
        //Properties.
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
    }
}
