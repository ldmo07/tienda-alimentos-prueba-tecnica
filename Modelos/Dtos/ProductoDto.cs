namespace Modelos.Dtos
{
    public class ProductoDto
    {
        public Guid? idProducto { get; set; } = default(Guid?);
        public Guid idCategoria { get; set; }
        public string nombreProducto { get; set; }
        public Decimal precioProducto { get; set; }
        public int unidadesDisponibles { get; set; }
        public bool? isDisponible { get; set; }
    }
}
