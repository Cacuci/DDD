using MediatR;
using Store.Catalogo.Domain.Repository;
using Store.Catalogo.Domain.Services;
using Store.Core.Communication.Mediator;
using Store.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Catalogo.Domain.Events
{
    public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>,
      INotificationHandler<PedidoIniciadoEvent>,
      INotificationHandler<PedidoProcessamentoCanceladoEvent>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMediatorHandler _mediatorHandler;

        public ProdutoEventHandler(IProdutoRepository produtoRepository, IEstoqueService estoqueService)
        {
            _produtoRepository = produtoRepository;
            _estoqueService = estoqueService;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent messagem, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorID(messagem.AggregateID);
            
            //Enviar email para aquisição de mais produtos
        }

        public async Task Handle(PedidoIniciadoEvent messagem, CancellationToken cancellationToken)
        {
            var result = await _estoqueService.DebitarPedidoProdutos(messagem.Itens);

            if (result)
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueConfirmadoEvent(messagem.PedidoID, messagem.ClienteID, messagem.Total, messagem.NomeCartao, messagem.NumeroCartao, messagem.ExpiracaoCartao, messagem.CvvCartao, messagem.Itens));
            }
            else
            {
                await _mediatorHandler.PublicarEvento(new PedidoEstoqueRejeitadoEvent(messagem.PedidoID, messagem.ClienteID));
            }
        }

        public async Task Handle(PedidoProcessamentoCanceladoEvent mensagem, CancellationToken cancellationToken)
        {
            await _estoqueService.ReporItemsEstoque(mensagem.PedidoItem);
        }
    }
}
