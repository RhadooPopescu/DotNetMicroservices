using System.Threading.Tasks;
using WebClient.Models;

namespace WebClient.Services
{
    //This interface will perform the cosume operations for the Basket microservices.
    public interface IBasketService
    {
        //This method will get the shopping basket for the given the username, 
        //and cosume the basket list into the BasketModel.
        Task<BasketModel> GetBasket(string userName);

        //This method will update the shopping basket, 
        //and cosume the basket list into the BasketModel.
        Task<BasketModel> UpdateBasket(BasketModel model);

        //This method will checkout the shopping basket, 
        //according to the BasketCheckoutModel.
        Task CheckoutBasket(BasketCheckoutModel model);
    }
}
