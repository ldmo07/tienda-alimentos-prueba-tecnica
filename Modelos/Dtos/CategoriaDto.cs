namespace Modelos.Dtos
{
    public class CategoriaDto
    {
        public Guid? idCategoria { get; set; } = default(Guid?);
        public string nombreCategoria { get; set; }
        public string? descripcionCategoria { get; set; }
    }
}
