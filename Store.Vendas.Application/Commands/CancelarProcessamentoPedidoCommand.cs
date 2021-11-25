using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Commands
{
    public class CancelarProcessamentoPedidoCommand : Command
    {
        public Guid PedidoID { get; set; }
        public Guid ClienteID { get; set; }

        public CancelarProcessamentoPedidoCommand(Guid pedidoID, Guid clienteID)
        {
            AggregateID = pedidoID;
            PedidoID = pedidoID;
            ClienteID = clienteID;
        }
    }
}
