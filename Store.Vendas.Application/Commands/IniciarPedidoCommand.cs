using FluentValidation;
using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Commands
{
    public class IniciarPedidoCommand : Command
    {
        public Guid PedidoID { get; private set; }
        public Guid ClienteID { get; private set; }
        public decimal Total { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string ExpiracaoCartao { get; private set; }
        public string CvvCartao { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new IniciarPedidoValidaton().Validate(this);

            return ValidationResult.IsValid;
        }

        public IniciarPedidoCommand(Guid pedidoID, Guid clienteID, decimal total, string nomeCartao, string numeroCartao, string expiracaoCartao, string cvvCartao)
        {
            PedidoID = pedidoID;
            ClienteID = clienteID;
            Total = total;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            ExpiracaoCartao = expiracaoCartao;
            CvvCartao = cvvCartao;
        }

        public class IniciarPedidoValidaton : AbstractValidator<IniciarPedidoCommand>
        {
            public IniciarPedidoValidaton()
            {
                RuleFor(c => c.ClienteID)
                    .NotEqual(Guid.Empty)
                    .WithMessage("ID do cliente inválido");

                RuleFor(c => c.PedidoID)
                    .NotEqual(Guid.Empty)
                    .WithMessage("ID do pedido inválido");

                RuleFor(c => c.NomeCartao)
                    .NotEmpty()
                    .WithMessage("O nome no cartão não foi informado");

                RuleFor(c => c.NumeroCartao)
                    .CreditCard()
                    .WithMessage("Número de cartão de crédito inválido");

                RuleFor(c => c.ExpiracaoCartao)
                    .NotEmpty()
                    .WithMessage("Data de expiração não informada");

                RuleFor(c => c.CvvCartao)
                    .Length(min: 3, max: 4)
                    .WithMessage("O CVV não foi preenchido corretamente");
            }
        }
    }    
}
