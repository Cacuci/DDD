using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Commands
{
    public class CancelarPedidoCommand : Command
    {
        public Guid PedidoID { get; private set; }
        public Guid ClienteID { get; private set; }

        public CancelarPedidoCommand(Guid pedidoID, Guid clienteID)
        {
            AggregateID = pedidoID;
            PedidoID = pedidoID;
            ClienteID = clienteID;
        }
    }
}
