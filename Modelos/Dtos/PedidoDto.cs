namespace Modelos.Dtos
{
    public class PedidoDto
    {
        public Guid? idPedido { get; set; } = default(Guid?);
        public Guid idUsuario { get; set; }
        public string direccionEnvio { get; set; }
        public bool? isPagado { get; set; }
        public bool? isEliminado { get; set; }
    }
}
