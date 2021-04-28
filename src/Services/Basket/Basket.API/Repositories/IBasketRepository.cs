using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    //This interface will perform the crud operation over the ShoppingBasket.
    public interface IBasketRepository
    {
        //This method will get the basket with a provided username as a parameter.
        Task<ShoppingBasket> GetBasket(string userName);

        //This method will update the shopping basket with passing the basket object.
        Task<ShoppingBasket> UpdateBasket(ShoppingBasket basket);

        //This method will delete the basket for the provided username as a parameter.
        Task DeleteBasket(string userName);
    }
}
