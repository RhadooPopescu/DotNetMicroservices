using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class ShoppingBasket
    {
        //fields
        public string UserName { get; set; }
        public List<ShoppingBasketItem> Items { get; set; } = new List<ShoppingBasketItem>();

        //constructors
        public ShoppingBasket()
        {
        }
        public ShoppingBasket(string userName)
        {
            UserName = userName;    
        }

        //this method calculates the total price of the items that are in the shopping-basket
        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }
                return totalprice;
            }
        }
    }
}
