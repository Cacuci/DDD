using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Events
{
    public class PedidoFinalizadoEvent : Event
    {
        public Guid PedidoID { get; private set; }

        public PedidoFinalizadoEvent(Guid pedidoID)
        {
            AggregateID = pedidoID;
            PedidoID = pedidoID;
        }
    }
}
