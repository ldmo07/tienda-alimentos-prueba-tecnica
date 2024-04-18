namespace Modelos.Dtos
{
    public class DetalleCompraDto
    {
        public Guid idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public Guid idPedido { get; set; }
        public Guid idProducto { get; set; }
        public string nombreProducto { get; set; }
        public Decimal precioProducto { get; set; }
        public int unidadesPedidas { get; set; }
        public Decimal subTotalPedido { get; set; }
    }
}
