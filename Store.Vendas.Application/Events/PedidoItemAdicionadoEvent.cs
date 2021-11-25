using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Events
{
    public class PedidoItemAdicionadoEvent : Event
    {
        public Guid ClienteID { get; private set; }
        public Guid PedidoID { get; private set; }
        public Guid ProdutoID { get; private set; }
        public string ProdutoNome { get; set; }
        public decimal ValorUnitario { get; private set; }
        public decimal Quantidade { get; set; }

        public PedidoItemAdicionadoEvent(Guid clienteID, Guid pedidoID, Guid produtoID, string produtoNome, decimal valorUnitario, decimal quantidade)
        {
            AggregateID = pedidoID;
            ClienteID = clienteID;
            PedidoID = pedidoID;
            ProdutoID = produtoID;
            ProdutoNome = produtoNome;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
        }
    }
}
