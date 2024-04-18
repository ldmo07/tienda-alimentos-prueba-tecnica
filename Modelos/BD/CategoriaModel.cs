namespace Modelos.BD
{
    public class CategoriaModel
    {
        public Guid? idCategoria { get; set; }
        public string nombreCategoria { get; set; }
        public string? descripcionCategoria { get; set; }
        public bool? isEliminado { get; set; }
        public DateTime? fechaInsercion { get; set; }
    }
}
