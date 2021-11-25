using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Events
{
    public class VoucherAplicadoPedidoEvent : Event
    {
        public Guid ClienteID { get; private set; }
        public Guid PedidoID { get; private set; }
        public Guid VoucherID { get; private set; }    

        public VoucherAplicadoPedidoEvent(Guid clienteID, Guid pedidoID, Guid voucherID)
        {
            AggregateID = pedidoID;
            ClienteID = clienteID;
            PedidoID = pedidoID;
            VoucherID = voucherID;            
        }
    }
}
