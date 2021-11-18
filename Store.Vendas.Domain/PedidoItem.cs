using Store.Core.DomainObjects;
using System;

namespace Store.Vendas.Domain
{
    public class PedidoItem : Entity
    {
        public Guid PedidoID { get; private set; }
        public Guid ProdutoID { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        //EF Rel.
        public Pedido Pedido { get; set; }

        public PedidoItem(Guid produtoID, string produtoNome, int quantidade, decimal valorUnitario)
        {
            ProdutoID = produtoID;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        protected PedidoItem()
        {
        }

        internal void AssociarPedido(Guid pedidoID)
        {
            PedidoID = pedidoID;
        }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }

        public override bool EhValido()
        {
            return true;
        }
    }
}