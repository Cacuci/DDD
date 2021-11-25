using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Events
{
    public class PedidoProdutoAtualizadoEvent : Event
    {
        public Guid ClienteID { get; set; }
        public Guid PedidoID { get; set; }
        public Guid ProdutoID { get; set; }
        public int Quantidade { get; set; }

        public PedidoProdutoAtualizadoEvent(Guid clienteID, Guid pedidoID, Guid produtoID, int quantidade)
        {
            AggregateID = pedidoID;
            ClienteID = clienteID;
            PedidoID = pedidoID;
            ProdutoID = produtoID;
            Quantidade = quantidade;
        }
    }
}
