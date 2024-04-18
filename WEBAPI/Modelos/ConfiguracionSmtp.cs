namespace WEBAPI.Modelos
{
    public class ConfiguracionSmtp
    {
        public string host { get; set; }
        public int port { get; set; }
        public string emailRemitente { get; set; }
        public string passwordEmailRemitente { get; set; }
    }

}
