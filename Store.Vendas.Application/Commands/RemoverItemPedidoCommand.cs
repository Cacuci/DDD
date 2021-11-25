using FluentValidation;
using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Commands
{
    public class RemoverItemPedidoCommand : Command
    {
        public Guid ClienteID { get; private set; }
        public Guid ProdutoID { get; private set; }        

        public RemoverItemPedidoCommand(Guid clienteID, Guid produtoID)
        {
            ClienteID = clienteID;
            ProdutoID = produtoID;            
        }

        public override bool EhValido()
        {
            ValidationResult = new RemoverItemPedidoValidation().Validate(instance: this);

            return ValidationResult.IsValid;
        }

        public class RemoverItemPedidoValidation : AbstractValidator<RemoverItemPedidoCommand>
        {
            public RemoverItemPedidoValidation()
            {
                RuleFor(expression: c => c.ClienteID)
                    .NotEqual(toCompare: Guid.Empty)
                    .WithMessage("ID do cliente inválido");

                RuleFor(c => c.ProdutoID)
                    .NotEqual(Guid.Empty)
                    .WithMessage("ID do produto inválido");                
            }
        }
    }
}
