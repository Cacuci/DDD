using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Events
{
    public class PedidoRascunhoIniciadoEvent : Event
    {
        public Guid ClienteID { get; private set; }
        public Guid PedidoID { get; private set; }

        public PedidoRascunhoIniciadoEvent(Guid clienteID, Guid pedidoID)
        {
            AggregateID = pedidoID;
            ClienteID = clienteID;
            PedidoID = pedidoID;
        }
    }
}
