namespace Modelos.BD
{
    public class ProductoModel
    {
        public Guid? idProducto { get; set; }
        public Guid idCategoria { get; set; }
        public string nombreProducto { get; set; }
        public Decimal precioProducto { get; set; }
        public int unidadesDisponibles { get; set; }
        public bool? isDisponible { get; set; }
        public bool? isEliminado { get; set; }
        public DateTime? fechaInsercion { get; set; }
    }
}
