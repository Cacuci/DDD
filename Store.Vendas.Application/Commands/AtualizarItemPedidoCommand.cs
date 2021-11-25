using FluentValidation;
using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Commands
{
    public class AtualizarItemPedidoCommand : Command
    {
        public Guid ClienteID { get; private set; }
        public Guid ProdutoID { get; private set; }        
        public int Quantidade { get; set; }        

        public AtualizarItemPedidoCommand(Guid clienteID, Guid produtoID,int quantidade)
        {
            ClienteID = clienteID;
            ProdutoID = produtoID;            
            Quantidade = quantidade;            
        }

        public override bool EhValido()
        {
            ValidationResult = new AtualizarItemPedidoValidation().Validate(instance: this);
            
            return ValidationResult.IsValid;
        }
    }

    public class AtualizarItemPedidoValidation : AbstractValidator<AtualizarItemPedidoCommand>
    {
        public AtualizarItemPedidoValidation()
        {
            RuleFor(expression: c => c.ClienteID)
                .NotEqual(toCompare: Guid.Empty)
                .WithMessage("ID do cliente inválido");

            RuleFor(c => c.ProdutoID)
                .NotEqual(Guid.Empty)
                .WithMessage("ID do produto inválido");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(c => c.Quantidade)
                .LessThan(16)
                .WithMessage("A quantidade máxima de um item é 15");
        }
    }
}

