using Store.Vendas.Application.Queries.ViewModels;
using Store.Vendas.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Vendas.Application.Queries
{
    public class PedidoQueries : IPedidoQueries
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoQueries(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<CarrinhoViewModel> ObterCarrinhoCliente(Guid clienteId)
        {
            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteID(clienteId);

            if (pedido is null)
            {
                return null;
            }

            var carrinho = new CarrinhoViewModel()
            {
                ClienteID = pedido.ClienteID,
                ValorTotal = pedido.ValorTotal,
                PedidoID = pedido.ID,
                ValorDesconto = pedido.Desconto,
                SubTotal = pedido.Desconto + pedido.ValorTotal
            };

            return carrinho;
        }

        public async Task<IEnumerable<PedidoViewModel>> ObterPedidosCliente(Guid clienteId)
        {
            var pedidos = await _pedidoRepository.ObterListaPorClienteID(clienteId);

            if (!pedidos.Any())
            {
                return null;
            }

            var pedidosView = new List<PedidoViewModel>();

            foreach (var pedido in pedidos)
            {
                pedidosView.Add(new PedidoViewModel()
                {
                    ValorTotal = pedido.ValorTotal,
                    PedidoStatus = (int)pedido.PedidoStatus,
                    Codigo = pedido.Codigo,
                    DataCadastro = pedido.DadaCadastro
                });
            }

            return pedidosView;
        }
    }
}
