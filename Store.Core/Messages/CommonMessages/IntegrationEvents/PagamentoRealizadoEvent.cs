using System;

namespace Store.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PagamentoRealizadoEvent : IntegrationEvent
    {
        public Guid PedidoID { get; private set; }
        public Guid ClienteID { get; private set; }
        public Guid PagamentoID { get; private set; }
        public Guid TransacaoID { get; private set; }
        public decimal Total { get; private set; }

        public PagamentoRealizadoEvent(Guid pedidoID, Guid clienteID, Guid pagamentoID, Guid transacaoID, decimal total)
        {
            AggregateID = pagamentoID;
            PedidoID = pedidoID;
            ClienteID = clienteID;
            PagamentoID = pagamentoID;
            TransacaoID = transacaoID;
            Total = total;
        }
    }
}
