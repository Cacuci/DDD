using Microsoft.EntityFrameworkCore;
using Store.Core.Data;
using Store.Vendas.Domain.Models;
using Store.Vendas.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.Vendas.Domain.Models.PedidoStatus;

namespace Store.Vendas.Data
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly VendasContext _context;

        public PedidoRepository(VendasContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnityOfWork => _context;

        public async Task<Pedido> ObterPorId(Guid id)
        {
            return await _context.Pedidos.FindAsync(id);
        }

        public async Task<IEnumerable<Pedido>> ObterListaPorClienteID(Guid clienteId)
        {
            return await _context.Pedidos.AsNoTracking().Where(p => p.ClienteID == clienteId).ToListAsync();
        }

        public async Task<Pedido> ObterPedidoRascunhoPorClienteID(Guid clienteId)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.ClienteID == clienteId && p.PedidoStatus == EPedidoStatus.Rascunho);

            if (pedido == null)
            {
                return null;
            } 

            await _context.Entry(pedido)
                .Collection(i => i.PedidoItems).LoadAsync();

            if (pedido.VoucherID != null)
            {
                await _context.Entry(pedido)
                    .Reference(i => i.Voucher).LoadAsync();
            }

            return pedido;
        }

        public void Adicionar(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
        }

        public void Atualizar(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
        }

        public async Task<PedidoItem> ObterItemPorID(Guid id)
        {
            return await _context.PedidoItems.FindAsync(id);
        }

        public async Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId)
        {
            return await _context.PedidoItems.FirstOrDefaultAsync(p => p.ProdutoID == produtoId && p.PedidoID == pedidoId);
        }

        public void AdicionarItem(PedidoItem pedidoItem)
        {
            _context.PedidoItems.Add(pedidoItem);
        }

        public void AtualizarItem(PedidoItem pedidoItem)
        {
            _context.PedidoItems.Update(pedidoItem);
        }

        public void RemoverItem(PedidoItem pedidoItem)
        {
            _context.PedidoItems.Remove(pedidoItem);
        }

        public async Task<Voucher> ObterVoucherPorCodigo(string codigo)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Codigo == codigo);
        }

        public async Task<PedidoItem> ObterItemPorId(Guid id)
        {
            return await _context.PedidoItems.FindAsync(id);
        }

        public async Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clienteId)
        {
            var pedido = _context.Pedidos.FirstOrDefault(p => p.ClienteID == clienteId && p.PedidoStatus == EPedidoStatus.Rascunho);

            if (pedido == null)
            {
                return null;
            } 

            await _context.Entry(pedido)
                .Collection(i => i.PedidoItems).LoadAsync();

            if (pedido.VoucherID != null)
            {
                await _context.Entry(pedido)
                    .Reference(i => i.Voucher).LoadAsync();
            }

            return pedido;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}