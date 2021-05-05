using System;

namespace Ordering.Application.Exceptions
{
    //This class represents our custom exception in case an order is not found in the database.
    public class NotFoundException : ApplicationException
    {
        //Entity name which is Order and key which is the order id.
        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found in the database.")
        {
        }
    }
}
