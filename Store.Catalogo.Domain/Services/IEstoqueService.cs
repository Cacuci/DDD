using System;
using System.Threading.Tasks;

namespace Store.Catalogo.Domain.Services
{
    public interface IEstoqueService : IDisposable
    {
        Task<bool> DebitarEstoqueAsync(Guid produtoID, int quantidade);

        Task<bool> ReporEstoqueAsync(Guid produtoID, int quantidade);
    }
}
