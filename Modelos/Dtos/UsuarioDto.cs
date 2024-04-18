namespace Modelos.Dtos
{
    public class UsuarioDto
    {
        public Guid? idUsuario { get; set; } = default(Guid?);
        public string nombreUsuario { get; set; }
        public string apellidoUsuario { get; set; }
        public string nidUsuario { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string correoUsuario { get; set; }
        public string rolUsuario { get; set; }
    }
}
