using System;

namespace Ordering.Domain.Common
{
    //This class will provide the fileds for the entities related to the orders that will be defined in the ordering microservices.
    public abstract class EntityBase
    {
        //Id also known as a primary key, protected set required in order to use the derived classes.
        public int Id { get; protected set; }

        //What user created the order and when it was created.
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        //Update order properties.
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
