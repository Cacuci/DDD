using FluentValidation;
using Store.Core.Messages;
using System;

namespace Store.Vendas.Application.Commands
{
    public class AplicarVoucherPedidoCommand : Command
    {
        public Guid ClienteID { get; private set; }        
        public string CodigoVoucher { get; set; }

        public AplicarVoucherPedidoCommand(Guid clienteID, string codigoVoucher)
        {
            ClienteID = clienteID;            
            CodigoVoucher = codigoVoucher;
        }

        public override bool EhValido()
        {
            ValidationResult = new AplicarVoucherPedidoValidation().Validate(instance: this);

            return ValidationResult.IsValid;
        }

        public class AplicarVoucherPedidoValidation : AbstractValidator<AplicarVoucherPedidoCommand>
        {
            public AplicarVoucherPedidoValidation()
            {
                RuleFor(expression: c => c.ClienteID)
                    .NotEqual(toCompare: Guid.Empty)
                    .WithMessage("ID do cliente inválido");                

                RuleFor(c => c.CodigoVoucher)
                    .NotEmpty()
                    .WithMessage("O código do voucher não pode ser vazio");
            }
        }
    }
}
