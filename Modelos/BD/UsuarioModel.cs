namespace Modelos.BD
{
    public class UsuarioModel
    {
        public Guid? idUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string apellidoUsuario { get; set; }
        public string nidUsuario { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string correoUsuario { get; set; }
        public string rolUsuario { get; set; }
        public bool? isEliminado { get; set; }
        public DateTime? fechaInsercion { get; set; }
    }
}
