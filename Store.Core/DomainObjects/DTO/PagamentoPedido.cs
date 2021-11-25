using System;

namespace Store.Core.DomainObjects.DTO
{
    public class PagamentoPedido
    {
        public Guid PedidoID { get; set; }
        public Guid ClienteID { get; set; }
        public decimal Total { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string ExpiracaoCartao { get; set; }
        public string CvvCartao { get; set; }
    }
}
