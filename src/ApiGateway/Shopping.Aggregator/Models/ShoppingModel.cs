using System.Collections.Generic;

namespace Shopping.Aggregator.Models
{
    //This class provides a route model for the response of the client application.
    //This will be our final model class that will return a result according to the client request.
    //Basically this is the aggregate response, after colleting data from our microservices.

    public class ShoppingModel
    {
        //Properties.
        public string UserName { get; set; }
        public BasketModel BasketWithProducts { get; set; }
        public IEnumerable<OrderResponseModel> Orders { get; set; }
    }
}
