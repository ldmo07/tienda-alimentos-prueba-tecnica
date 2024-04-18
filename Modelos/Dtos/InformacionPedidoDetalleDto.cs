namespace Modelos.Dtos
{
    public class InformacionPedidoDetalleDto
    {
        public PedidoDto pedido { get; set; }
        public List<DetallePedidoDto> detallesPedido { get; set; }
    }
}
