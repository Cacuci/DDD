using Store.Core.DomainObjects;
using System;

namespace Store.Catalago.Domain.Events
{
    public class ProdutoAbaixoEstoqueEvent : DomainEvent
    {
        public int QuantidadeRestante { get; private set; }

        public ProdutoAbaixoEstoqueEvent(Guid aggregateID, int quantidadeRestante) : base (aggregateID)   
        {
            QuantidadeRestante = quantidadeRestante;
        }
    }
}
