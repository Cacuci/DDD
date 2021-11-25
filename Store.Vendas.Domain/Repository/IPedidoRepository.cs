using Store.Core.Data;
using Store.Vendas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Vendas.Domain.Repository
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<Pedido> ObterPorId(Guid id);
        Task<IEnumerable<Pedido>> ObterListaPorClienteID(Guid clienteId);
        Task<Pedido> ObterPedidoRascunhoPorClienteID(Guid clienteId);
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);

        Task<PedidoItem> ObterItemPorID(Guid id);
        Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId);
        void AdicionarItem(PedidoItem pedidoItem);
        void AtualizarItem(PedidoItem pedidoItem);
        void RemoverItem(PedidoItem pedidoItem);

        Task<Voucher> ObterVoucherPorCodigo(string codigo);
    }
}
