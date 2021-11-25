using MediatR;
using Store.Core.Communication.Mediator;
using Store.Core.Messages.CommonMessages.IntegrationEvents;
using Store.Vendas.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Vendas.Application.Events
{
    public class PedidoEventHandler : INotificationHandler<PedidoRascunhoIniciadoEvent>,
      INotificationHandler<PedidoAtualizadoEvent>,
      INotificationHandler<PedidoItemAdicionadoEvent>,
      INotificationHandler<PedidoEstoqueRejeitadoEvent>,
      INotificationHandler<PagamentoRealizadoEvent>,
      INotificationHandler<PagamentoRecusadoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(PedidoRascunhoIniciadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        public Task Handle(PedidoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoItemAdicionadoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(PedidoEstoqueRejeitadoEvent mensagem, CancellationToken cancellationToken)
        {
            return _mediatorHandler.EnviarComando(new CancelarProcessamentoPedidoCommand(mensagem.PedidoID, mensagem.ClienteID));
        }        

        public async Task Handle(PagamentoRealizadoEvent mensagem, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new FinalizarPedidoCommand(mensagem.PedidoID, mensagem.ClienteID));
        }

        public async Task Handle(PagamentoRecusadoEvent mensagem, CancellationToken cancellationToken)
        {
            await _mediatorHandler.EnviarComando(new CancelarPedidoCommand(mensagem.PedidoID, mensagem.ClienteID));
        }
    }
}
