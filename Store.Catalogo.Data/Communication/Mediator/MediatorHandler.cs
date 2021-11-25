using MediatR;
using Store.Core.Messages;
using Store.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace Store.Catalogo.Data.Communication.Mediator
{
    class MediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> EnviarComando<T>(T comando) where T : Microsoft.EntityFrameworkCore.DbLoggerCategory.Database.Command
        {
            return (bool)await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            await _mediator.Publish(notificacao);
        }
    }
}
