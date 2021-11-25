using MediatR;
using Store.Core.DomainObjects.DTO;
using Store.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Pagamentos.Business.Events
{
    public class PagamentoEventHandler : INotificationHandler<PedidoEstoqueConfirmadoEvent>
    {
        private readonly IPagamentoService _pagamentoService;

        public PagamentoEventHandler(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        public async Task Handle(PedidoEstoqueConfirmadoEvent messagem, CancellationToken cancellationToken)
        {
            var pagamentoPedido = new PagamentoPedido()
            {
                PedidoID = messagem.PedidoID,
                ClienteID = messagem.ClienteID,
                Total = messagem.Total,
                NomeCartao = messagem.NomeCartao,
                NumeroCartao = messagem.NumeroCartao,
                ExpiracaoCartao = messagem.ExpiracaoCartao,
                CvvCartao = messagem.CvvCartao
            };

            await _pagamentoService.RealizarPagamentoPedido(pagamentoPedido);
        }
    }
}
