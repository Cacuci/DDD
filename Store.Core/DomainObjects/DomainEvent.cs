using Store.Core.Messages;
using System;

namespace Store.Core.DomainObjects
{
    public class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateID)
        {
            AggregateID = aggregateID;
        }
    }
}
