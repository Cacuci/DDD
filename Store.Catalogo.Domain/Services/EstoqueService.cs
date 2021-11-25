using Store.Catalogo.Domain.Events;
using Store.Catalogo.Domain.Repository;
using Store.Core.Communication.Mediator;
using Store.Core.DomainObjects.DTO;
using Store.Core.Messages.CommonMessages.Notifications;
using System;
using System.Threading.Tasks;

namespace Store.Catalogo.Domain.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public EstoqueService(IProdutoRepository produtoRepository, IMediatorHandler mediatr)
        {
            _produtoRepository = produtoRepository;
            _mediatorHandler = mediatr;
        }

        private async Task<bool> DebitarItemEstoque(Guid produtoID, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorID(produtoID);

            if (produto is null)
            {
                return false;
            }

            if (!produto.PossuiEstoque(quantidade))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("Estoque", $"Produto - {produto.Nome} sem estoque"));

                return false;
            }

            produto.DebitarEstoque(quantidade);

            if (produto.QuantidadeEstoque < 10)
            {
                await _mediatorHandler.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.ID, produto.QuantidadeEstoque));
            }

            _produtoRepository.Atualizar(produto);

            return true;
        }

        public async Task<bool> DebitarEstoque(Guid produtoID, int quantidade)
        {   
            if (!await DebitarItemEstoque(produtoID, quantidade))
            {
                return false;
            }
            
            return await _produtoRepository.UnityOfWork.Commit();
        }

        public async Task<bool> DebitarPedidoProdutos(PedidoItem pedidoItem)
        {
            foreach (var item in pedidoItem.Itens)
            {
                if (!await DebitarItemEstoque(item.ID, item.Quantidade))
                {
                    return false;
                }
            }

            return await _produtoRepository.UnityOfWork.Commit();
        }        

        private async Task<bool> ReporItemEstoque(Guid produtoID, int quantidade)
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

        public async Task<bool> ReporEstoque(Guid produtoID, int quantidade)
        {
            var sucesso = await ReporItemEstoque(produtoID, quantidade);

            if (!sucesso)
            {
                return false;
            }           

            return await _produtoRepository.UnityOfWork.Commit();
        }

        public async Task<bool> ReporItemsEstoque(PedidoItem pedidoItem)
        {
            foreach (var item in pedidoItem.Itens)
            {
                if (!await ReporItemEstoque(item.ID, item.Quantidade))
                {
                    return false;
                }
            }

            return await _produtoRepository.UnityOfWork.Commit();
        }

        public void Dispose() => _produtoRepository.Dispose();
    }
}
