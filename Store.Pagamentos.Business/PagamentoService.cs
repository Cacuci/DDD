using Store.Core.Communication.Mediator;
using Store.Core.DomainObjects.DTO;
using Store.Core.Messages.CommonMessages.IntegrationEvents;
using Store.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace Store.Pagamentos.Business
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PagamentoService(IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade, IPagamentoRepository pagamentoRepository, IMediatorHandler mediatorHandler)
        {
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
            _pagamentoRepository = pagamentoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transacao> RealizarPagamentoPedido(PagamentoPedido pagamentoPedido)
        {
            var pedido = new Pedido
            {
                ID = pagamentoPedido.PedidoID,
                Valor = pagamentoPedido.Total
            };

            var pagamento = new Pagamento
            {
                Valor = pagamentoPedido.Total,
                NomeCartao = pagamentoPedido.NomeCartao,
                NumeroCartao = pagamentoPedido.NumeroCartao,
                ExpiracaoCartao = pagamentoPedido.ExpiracaoCartao,
                CvvCartao = pagamentoPedido.CvvCartao,
                PedidoID = pagamentoPedido.PedidoID
            };

            var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(pedido, pagamento);

            if (transacao.StatusTransacao == EStatusTransacao.Pago)
            {
                pagamento.AdicionarEvento(new PagamentoRealizadoEvent(pedido.ID, pagamentoPedido.ClienteID, transacao.PagamentoID, transacao.ID, pedido.Valor));

                _pagamentoRepository.Adicionar(pagamento);

                _pagamentoRepository.AdicionarTransacao(transacao);

                await _pagamentoRepository.UnityOfWork.Commit();

                return transacao;
            }

            await _mediatorHandler.PublicarNotificacao(new DomainNotification("pagamento", "A operadora recusou o pagamento"));
            
            await _mediatorHandler.PublicarEvento(new PagamentoRecusadoEvent(pedido.ID, pagamentoPedido.ClienteID, transacao.PagamentoID, transacao.ID, pedido.Valor));

            return transacao;
        }
    }
}
