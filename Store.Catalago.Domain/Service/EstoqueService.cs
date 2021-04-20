using Store.Catalago.Domain.Repository;
using System;
using System.Threading.Tasks;

namespace Store.Catalago.Domain.Service
{
    public class EstoqueService : IDisposable
    {
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> DebitarEstoqueAsync(Guid produtoID, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorID(produtoID);

            if (produto is null)
            {
                return false;
            }

            if (!produto.PossuiEstoque(quantidade))
            {
                return false;
            }

            produto.DebitarEstoque(quantidade);

            _produtoRepository.Atualizar(produto);

            return await _produtoRepository.UnityOfWork.Commit();
        }

        public async Task<bool> ReporEstoqueAsync(Guid produtoID, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorID(produtoID);

            if (produto is null)
            {
                return false;
            }

            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);

            return await _produtoRepository.UnityOfWork.Commit();
        }

        public void Dispose() => _produtoRepository.Dispose();
    }
}
