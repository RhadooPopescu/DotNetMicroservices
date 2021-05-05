namespace WebClient.Models
{
    //This class stored the basket item dto related properties.
    public class BasketItemModel
    {
        //Properties.
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
