using Store.Core.DomainObjects.DTO;
using System;

namespace Store.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoProcessamentoCanceladoEvent : Event
    {
        public Guid PedidoID { get; private set; }
        public Guid ClienteID { get; private set; }
        public PedidoItem PedidoItem { get; private set; }

        public PedidoProcessamentoCanceladoEvent(Guid pedidoID, Guid clienteID, PedidoItem pedidoItem)
        {
            AggregateID = pedidoID;
            PedidoID = pedidoID;
            ClienteID = clienteID;
            PedidoItem = pedidoItem;
        }
    }
}
