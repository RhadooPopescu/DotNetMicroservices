using Shopping.Aggregator.Models;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    //This interface will perform the cosume operations for the Basket microservices.
    public interface IBasketService
    {
        //This method will get the shopping basket for the given the username, 
        //and store the basket list into the BasketModel.
        Task<BasketModel> GetBasket(string userName);
    }
}
