using Store.Core.DomainObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Store.Vendas.Domain.PedidoStatus;

namespace Store.Vendas.Domain
{
    public class Pedido : Entity, IAggregateRoot
    {
        public int Codigo { get; private set; }
        public Guid ClienteID { get; private set; }
        public Guid? VoucherID { get; private set; }
        public bool VoucherUtilizado { get; set; }
        public decimal Desconto { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DadaCadastro { get; private set; }
        public EPedidoStatus PedidoStatus { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;

        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        //EF Rel.
        public virtual Voucher Voucher { get; set; }

        public Pedido(Guid clienteID, bool voucherUtilizado, decimal desconto,
            decimal valorTotal)
        {
            ClienteID = clienteID;
            VoucherUtilizado = voucherUtilizado;
            Desconto = desconto;
            ValorTotal = valorTotal;
            _pedidoItems = new List<PedidoItem>();
        }

        protected Pedido()
        {
            _pedidoItems = new List<PedidoItem>();
        }

        public void AplicarVoucher(Voucher voucher)
        {
            Voucher = voucher;
            
            VoucherUtilizado = true;
            
            CalcularValorPedido();
        }

        public void CalcularValorPedido()
        {
            ValorTotal = PedidoItems.Sum(c => c.CalcularValor());

            CalcularValorTotalDesconto();
        }

        public decimal CalcularDescontoPorPorcentagem()
        {
            if (!Voucher.Percentual.HasValue)
            {
                return 0;
            }

            return ValorTotal * Voucher.Percentual.Value / 100;
        }

        public decimal CalcularDescontoPorValor()
        {
            if (!Voucher.ValorDesconto.HasValue)
            {
                return 0;
            }

            return Voucher.ValorDesconto.Value;            
        }        

        public decimal CalcularValorTotalComDesconto()
        {
            return ValorTotal - Desconto;
        }

        public void CalcularValorTotalDesconto()
        {
            if (!VoucherUtilizado)
            {
                return;
            }

            Desconto = Voucher.TipoDescontoVoucher switch
            {
                TipoDescontoVoucher.ETipoDescontoVoucher.Procentagem => CalcularDescontoPorPorcentagem(),
                _ => CalcularDescontoPorValor() 
            };

            ValorTotal = CalcularValorTotalComDesconto() < 0 ? 0 : CalcularValorTotalComDesconto();
        }

        public bool PedidoItemExistente(PedidoItem item)
        {
            return _pedidoItems.Any(c => c.ProdutoID == item.ProdutoID);
        }

        public void AdicionarItem(PedidoItem item)
        {
            if (!item.EhValido())
            {
                return;
            }

            item.AssociarPedido(ID);

            if (PedidoItemExistente(item))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(c => c.ProdutoID == item.ProdutoID);

                itemExistente.AdicionarUnidades(item.Quantidade);

                item = itemExistente;

                _pedidoItems.Remove(itemExistente);
            }

            item.CalcularValor();

            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem item)
        {
            if (!item.EhValido())
            {
                return;
            }

            var itemExiste = PedidoItems.FirstOrDefault(c => c.ProdutoID == item.ProdutoID);

            if (itemExiste is null)
            {
                throw new DomainException(message: "O item não pertence ao pedido");
            }

            CalcularValorPedido();
        }

        public void AtualizarItem(PedidoItem item)
        {
            if (!item.EhValido())
            {
                return;
            }

            var itemExiste = PedidoItems.FirstOrDefault(c => c.ProdutoID == item.ProdutoID);

            if (itemExiste is null)
            {
                throw new DomainException(message: "O item não pertence ao pedido");
            }

            _pedidoItems.Remove(itemExiste)
                ;
            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        public void AtualizarUnidades(PedidoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);

            AtualizarItem(item);
        }

        public void TornarRascunho()
        {
            PedidoStatus = EPedidoStatus.Rascunho;
        }

        public void IniciarPedido()
        {
            PedidoStatus = EPedidoStatus.Iniciado;
        }

        public void FinalizarPedido()
        {
            PedidoStatus = EPedidoStatus.Pago;
        }

        public void CancelarPedido()
        {
            PedidoStatus = EPedidoStatus.Cancelado;
        }

        public static class PedidoFactory
        {
            public static Pedido NovoPedidoRascunho(Guid clienteID)
            {
                var pedido = new Pedido()
                {
                    ClienteID = clienteID
                };

                pedido.TornarRascunho();

                return pedido;
            }
        }
    }
}
