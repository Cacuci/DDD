using System;
using System.Collections.Generic;

namespace Store.Core.DomainObjects.DTO
{
    public class PedidoItem
    {
        public Guid PedidoID { get;  set; }
        public ICollection<Item> Itens { get; set; }
    }

    public class Item
    {
        public Guid ID { get; init; }
        public int Quantidade { get; init; }
    }
}
