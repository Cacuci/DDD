using Store.Core.DomainObjects;
using System;

namespace Store.Pagamentos.Business
{
    public class Transacao : Entity
    {
        public Guid PedidoID { get; set; }
        public Guid PagamentoID { get; set; }
        public decimal Total { get; set; }
        public EStatusTransacao StatusTransacao { get; set; }

        public Pagamento Pagamento { get; set; }
    }
}
