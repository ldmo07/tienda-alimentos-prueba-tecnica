namespace Modelos.Utils
{
    public class ParametrosCorreoModel
    {
        public string host { get; set; }
        public int port { get; set; }
        public string emailRemitente { get; set; }
        public string? passwordEmailRemitente { get; set; }
        public string emailReceptor { get; set; }
        public string asunto { get; set; }
        public string cuerpoEmail { get; set; }
    }
}
