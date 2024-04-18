namespace Modelos.BD
{
    public class PedidoModel
    {
        public Guid? idPedido { get; set; }
        public Guid idUsuario { get; set; }
        public string direccionEnvio { get; set; }
        public bool? isPagado { get; set; }
        public bool? isEliminado { get; set; }
        public DateTime? fechaInsercion { get; set; }
    }
}
