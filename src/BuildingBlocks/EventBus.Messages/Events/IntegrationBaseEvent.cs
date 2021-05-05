using System;

namespace EventBus.Messages.Events
{
    //This class handles the RabbitMq message events.
    public class IntegrationBaseEvent
    {
        //Common properties for handling messages.
        public Guid Id { get; private set; }

        public DateTime CreationDate { get; private set; }

        //Constructors.
        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

    }
}
