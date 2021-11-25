using System;
using System.Collections.Generic;

namespace Store.Pagamentos.Business
{
    public class Pedido
    {
        public Guid ID { get; set; }
        public decimal Valor { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
