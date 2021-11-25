using Store.Core.Messages.CommonMessages.DomainEvents;
using System;

namespace Store.Catalogo.Domain.Events
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
