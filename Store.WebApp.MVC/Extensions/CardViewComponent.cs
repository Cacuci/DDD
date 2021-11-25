using Microsoft.AspNetCore.Mvc;
using Store.Vendas.Application.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Extensions
{
    public class CardViewComponent : ViewComponent
    {
        private readonly IPedidoQueries _pedidoQueries;

        protected Guid ClienteID = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        public CardViewComponent(IPedidoQueries pedidoQueries)
        {
            _pedidoQueries = pedidoQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carrinho = await _pedidoQueries.ObterCarrinhoCliente(ClienteID);

            var itens = carrinho?.Items.Count ?? 0;

            return View(itens);
        }
    }
}
