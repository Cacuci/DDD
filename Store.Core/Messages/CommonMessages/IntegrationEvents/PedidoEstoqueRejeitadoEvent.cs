using System;

namespace Store.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoEstoqueRejeitadoEvent : Event
    {
        public Guid PedidoID { get; private set; }
        public Guid ClienteID { get; private set; }

        public PedidoEstoqueRejeitadoEvent(Guid pedidoID, Guid clienteID)
        {
            PedidoID = pedidoID;
            ClienteID = clienteID;            
        }
    }
}
