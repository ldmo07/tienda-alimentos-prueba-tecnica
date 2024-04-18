namespace Modelos.Dtos
{
    public class DetallePedidoDto
    {
        public Guid? idDetallePedido { get; set; } = default(Guid?);
        public Guid? idPedido { get; set; } = default(Guid?);
        public Guid idProducto { get; set; }
        public int unidadesPedidas { get; set; }
    }
}
