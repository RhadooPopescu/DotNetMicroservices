namespace Basket.API.Entities
{
    public class ShoppingBasketItem
    {
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string ProductId { get; set; }

        public string ProductName { get; set; }
    }
}
