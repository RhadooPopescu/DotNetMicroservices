using System.Collections.Generic;

namespace Basket.API.Entities
{
    //This class handles the items that are in the ShoppingBasket and calculates the total price of the basket.
    public class ShoppingBasket
    {
        //Properties, this will be stored in the redis database.
        public string UserName { get; set; }
        public List<ShoppingBasketItem> Items { get; set; } = new List<ShoppingBasketItem>();

        //Constructors.
        public ShoppingBasket()
        {
        }
        public ShoppingBasket(string userName)
        {
            UserName = userName;    
        }

        //This property/method calculates the total price of the items that are in the shopping-basket.
        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (ShoppingBasketItem item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }
                return totalprice;
            }
        }
    }
}
