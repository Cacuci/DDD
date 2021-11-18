using MediatR;
using Store.Catalogo.Domain.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Catalogo.Domain.Events
{
    public class ProdutoEventHandler : INotificationHandler<ProdutoAbaixoEstoqueEvent>
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoEventHandler(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Handle(ProdutoAbaixoEstoqueEvent messagem, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorID(messagem.AggregateID);
            
            //Enviar email para aquisição de mais produtos
        }
    }
}
