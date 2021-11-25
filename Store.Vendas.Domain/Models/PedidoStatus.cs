namespace Store.Vendas.Domain.Models
{
    public class PedidoStatus
    {
        public enum EPedidoStatus
        {
            Rascunho = 0,
            Iniciado = 1,
            Pago = 4,
            Entregue = 5,
            Cancelado = 6
        }
    }
}
