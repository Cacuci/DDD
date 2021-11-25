using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Events
{
    public class PedidoProdutoRemovidoEvent : Event
    {
        public Guid ClienteID { get; private set; }
        public Guid PedidoID { get; private set; }
        public Guid ProdutoID { get; private set; }

        public PedidoProdutoRemovidoEvent(Guid clienteID, Guid pedidoID, Guid produtoID)
        {
            AggregateID = pedidoID;
            ClienteID = clienteID;
            PedidoID = pedidoID;
            ProdutoID = produtoID;
        }   
    }
}
