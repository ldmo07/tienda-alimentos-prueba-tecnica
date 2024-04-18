namespace Modelos.BD
{
    public class DetallePedidoModel
    {
        public Guid? idDetallePedido { get; set; }
        public Guid idPedido { get; set; }
        public Guid idProducto { get; set; }
        public int unidadesPedidas { get; set; }
        public DateTime? fechaInsercion { get; set; }
    }
}
