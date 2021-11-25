using MediatR;
using Store.Core.Communication.Mediator;
using Store.Core.DomainObjects.DTO;
using Store.Core.Extensions;
using Store.Core.Messages;
using Store.Core.Messages.CommonMessages.IntegrationEvents;
using Store.Core.Messages.CommonMessages.Notifications;
using Store.Vendas.Application.Events;
using Store.Vendas.Domain.Models;
using Store.Vendas.Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Vendas.Application.Commands
{
    public class PedidoCommandHandler : IRequestHandler<AdicionarItemPedidoCommand, bool>,
        IRequestHandler<AtualizarItemPedidoCommand, bool>,
        IRequestHandler<RemoverItemPedidoCommand, bool>,
        IRequestHandler<AplicarVoucherPedidoCommand, bool>,
        IRequestHandler<IniciarPedidoCommand, bool>,
        IRequestHandler<FinalizarPedidoCommand, bool>,
        IRequestHandler<CancelarPedidoCommand, bool>,
        IRequestHandler<CancelarProcessamentoPedidoCommand, bool>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository, IMediatorHandler mediatorHandler)
        {
            _pedidoRepository = pedidoRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
            {
                return false;
            }

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteID(message.ClienteID);

            var pedidoItem = new Domain.Models.PedidoItem(message.ProdutoID, message.Nome, message.Quantidade, message.ValorUnitario);

            if (pedido is null)
            {
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.ClienteID);

                pedido.AdicionarItem(pedidoItem);

                _pedidoRepository.Adicionar(pedido);

                pedido.AdicionarEvento(new PedidoRascunhoIniciadoEvent(message.ClienteID, message.ProdutoID));
            }
            else
            {
                var pedidoExistente = pedido.PedidoItemExistente(pedidoItem);

                pedido.AdicionarItem(pedidoItem);

                if (pedidoExistente)
                {
                    _pedidoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(p => p.ProdutoID == pedidoItem.ProdutoID));
                }
                else
                {
                    _pedidoRepository.AdicionarItem(pedidoItem);
                }

                pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteID, pedido.ID, pedido.ValorTotal));
            }

            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(pedido.ClienteID, pedido.ID, message.ProdutoID, message.Nome, message.ValorUnitario, message.Quantidade));

            return await _pedidoRepository.UnityOfWork.Commit();
        }

        public async Task<bool> Handle(AtualizarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
            {
                return false;
            }

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteID(message.ClienteID);

            if (pedido is null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrrado!"));

                return false;
            }

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.ID, message.ProdutoID);

            if (!pedido.PedidoItemExistente(pedidoItem))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Item do pedido não encontrado!"));

                return false;
            }

            pedido.AtualizarUnidades(pedidoItem, message.Quantidade);

            pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteID, pedido.ID, pedido.ValorTotal));

            pedido.AdicionarEvento(new PedidoProdutoAtualizadoEvent(message.ClienteID, pedido.ID, message.ProdutoID, message.Quantidade));

            _pedidoRepository.RemoverItem(pedidoItem);

            _pedidoRepository.Atualizar(pedido);

            return await _pedidoRepository.UnityOfWork.Commit();
        }

        public async Task<bool> Handle(RemoverItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
            {
                return false;
            }

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteID(message.ClienteID);

            if (pedido is null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));

                return false;
            }

            var pedidoItem = await _pedidoRepository.ObterItemPorPedido(pedido.ID, message.ProdutoID);

            if (pedidoItem is null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Item do pedido não encontrado!"));

                return false;
            }

            pedido.RemoverItem(pedidoItem);

            pedido.AdicionarEvento(new PedidoAtualizadoEvent(message.ClienteID, pedido.ID, pedido.ValorTotal));

            pedido.AdicionarEvento(new PedidoProdutoRemovidoEvent(message.ClienteID, pedido.ID, message.ProdutoID));

            _pedidoRepository.RemoverItem(pedidoItem);

            _pedidoRepository.Atualizar(pedido);

            return await _pedidoRepository.UnityOfWork.Commit();
        }

        public async Task<bool> Handle(AplicarVoucherPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
            {
                return false;
            }

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteID(message.ClienteID);

            if (pedido is null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado!"));

                return false;
            }

            var voucher = await _pedidoRepository.ObterVoucherPorCodigo(message.CodigoVoucher);

            if (voucher == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "voucher não encontrado"));

                return false;
            }

            var voucherAplicacaoValidation = pedido.AplicarVoucher(voucher);

            if (!voucherAplicacaoValidation.IsValid)
            {
                foreach (var error in voucherAplicacaoValidation.Errors)
                {
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            pedido.AdicionarEvento(new PedidoAtualizadoEvent(pedido.ClienteID, pedido.ID, pedido.ValorTotal));

            pedido.AdicionarEvento(new VoucherAplicadoPedidoEvent(pedido.ClienteID, pedido.ID, voucher.ID));

            _pedidoRepository.Atualizar(pedido);

            return await _pedidoRepository.UnityOfWork.Commit();
        }

        public async Task<bool> Handle(IniciarPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message))
            {
                return false;
            }

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteID(message.ClienteID);

            pedido.IniciarPedido();

            var itens = new List<Item>();

            pedido.PedidoItems.ForEach(i => itens.Add(new Item { ID = i.ProdutoID, Quantidade = i.Quantidade }));

            var pedidoItems = new Core.DomainObjects.DTO.PedidoItem { PedidoID = pedido.ID, Itens = itens };

            pedido.AdicionarEvento(new PedidoIniciadoEvent(pedido.ID, pedido.ClienteID, pedido.ValorTotal, message.NomeCartao, message.NumeroCartao, message.ExpiracaoCartao, message.CvvCartao, pedidoItems));

            pedido.IniciarPedido();

            _pedidoRepository.Atualizar(pedido);

            return await _pedidoRepository.UnityOfWork.Commit();
        }

        public async Task<bool> Handle(FinalizarPedidoCommand mensagem, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorId(mensagem.PedidoID);

            if (pedido == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado"));

                return false;
            }

            pedido.FinalizarPedido();

            pedido.AdicionarEvento(new PedidoFinalizadoEvent(mensagem.PedidoID));

            return await _pedidoRepository.UnityOfWork.Commit();
        }

        public async Task<bool> Handle(CancelarPedidoCommand mensagem, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorId(mensagem.PedidoID);

            if (pedido == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado"));
            }

            var itens = new List<Item>();

            pedido.PedidoItems.ForEach(c =>
                itens.Add(new Item
                {
                    ID = c.ProdutoID,
                    Quantidade = c.Quantidade
                }));

            var produtos = new Core.DomainObjects.DTO.PedidoItem { PedidoID = pedido.ID, Itens = itens };

            pedido.AdicionarEvento(new PedidoProcessamentoCanceladoEvent(pedido.ID, pedido.ClienteID, produtos));

            pedido.TornarRascunho();

            return await _pedidoRepository.UnityOfWork.Commit();
        }

        public async Task<bool> Handle(CancelarProcessamentoPedidoCommand mensagem, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.ObterPorId(mensagem.PedidoID);

            if (pedido == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("pedido", "Pedido não encontrado"));

                return false;
            }

            pedido.TornarRascunho();

            return await _pedidoRepository.UnityOfWork.Commit();
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido())
            {
                return true;
            }

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
