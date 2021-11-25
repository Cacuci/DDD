using FluentValidation;
using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Commands
{
    public class AdicionarItemPedidoCommand : Command
    {
        public Guid ClienteID { get; private set; }
        public Guid ProdutoID { get; private set; }        
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        public AdicionarItemPedidoCommand(Guid clienteID, Guid produtoID, string nome, int quantidade, decimal valorUnitario)
        {
            ClienteID = clienteID;
            ProdutoID = produtoID;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public override bool EhValido()
        {
            var validationResult = new AdicionarItemPedidoValidation().Validate(instance: this);

            return validationResult.IsValid;
        }
    }

    public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
    {
        public AdicionarItemPedidoValidation()
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
                .LessThan(15)
                .WithMessage("A quantidade máxima de um item é 15");

            RuleFor(c => c.ValorUnitario)
                .GreaterThan(0)
                .WithMessage("O valor do item precisa ser maior que 0");
        }
    }
}
