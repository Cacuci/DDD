using System;

namespace Store.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; private set; }
        public Guid AggregateID { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
