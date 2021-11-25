using Store.Core.DomainObjects.DTO;
using System;
using System.Threading.Tasks;

namespace Store.Catalogo.Domain.Services
{
    public interface IEstoqueService : IDisposable
    {
        Task<bool> DebitarEstoque(Guid produtoID, int quantidade);
        Task<bool> DebitarPedidoProdutos(PedidoItem pedidoItem);
        Task<bool> ReporEstoque(Guid produtoID, int quantidade);
        Task<bool> ReporItemsEstoque(PedidoItem pedidoItem);
    }
}
