using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Events
{
    public class PedidoAtualizadoEvent : Event
    {
        public Guid ClienteID { get; private set; }
        public Guid PedidoID { get; private set; }
        public decimal ValorTotal { get; private set; }

        public PedidoAtualizadoEvent(Guid clienteID, Guid pedidoID, decimal valorTotal)
        {
            AggregateID = pedidoID;
            ClienteID = clienteID;
            PedidoID = pedidoID;
            ValorTotal = valorTotal;
        }
    }
}
