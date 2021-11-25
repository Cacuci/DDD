using Store.Core.DomainObjects.DTO;
using System;

namespace Store.Core.Messages.CommonMessages.IntegrationEvents
{
    public class PedidoIniciadoEvent : Event
    {
        public Guid PedidoID { get; private set; }
        public Guid ClienteID { get; private set; }
        public decimal Total { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }
        public PedidoItem Itens { get; private set; }

        public PedidoIniciadoEvent(Guid pedidoID, Guid clienteID, decimal total, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao, PedidoItem itens)
        {
            PedidoID = pedidoID;
            ClienteID = clienteID;
            Total = total;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
            Itens = itens;
        }
    }
}
