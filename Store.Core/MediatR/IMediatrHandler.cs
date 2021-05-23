using Store.Core.Messages;
using System.Threading.Tasks;

namespace Store.Core.MediatR
{
    public interface IMediatrHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
    }
}
