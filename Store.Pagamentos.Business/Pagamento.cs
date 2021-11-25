using Store.Core.DomainObjects;
using System;

namespace Store.Pagamentos.Business
{
    public class Pagamento : Entity, IAggregateRoot
    {
        public Guid MyProperty { get; set; }
        public Guid PedidoID { get; init; }
        public string Status { get; init; }
        public decimal Valor { get; init; }

        public string NomeCartao { get; init; }
        public string NumeroCartao { get; init; }
        public string ExpiracaoCartao { get; init; }
        public string CvvCartao { get; init; }

        //EF. Rel.
        public Transacao Transacao { get; init; }
    }
}
