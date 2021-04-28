namespace Basket.API.Entities
{
    //This class represents each item from the ShoppingBasket.
    public class ShoppingBasketItem
    {
        //Properties.
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }
}
