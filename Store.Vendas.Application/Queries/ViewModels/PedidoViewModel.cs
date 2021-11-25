using System;

namespace Store.Vendas.Application.Queries.ViewModels
{
    public class PedidoViewModel
    {
        public int Codigo { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCadastro { get; set; }
        public int PedidoStatus { get; set; }
    }
}