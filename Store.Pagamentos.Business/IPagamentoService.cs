using Store.Core.DomainObjects.DTO;
using System.Threading.Tasks;

namespace Store.Pagamentos.Business
{
    public interface IPagamentoService
    {
        Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido);
    }
}
