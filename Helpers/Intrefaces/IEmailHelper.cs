using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers.Intrefaces
{
    public interface IEmailHelper
    {
        /// <summary>
        /// Envia correo utilizando un host que no necesite Autenticacion
        /// </summary>
        /// <param name="host">host del smpt</param>
        /// <param name="port">puerto del smpt</param>
        /// <param name="emailRemitente">correo de quien envia</param>
        /// <param name="emailReceptor">correo de quien recibe</param>
        /// <param name="asunto">asunto del correo</param>
        /// <param name="cuerpoEmail">cuerpo o mensaje que se desea enviar</param>
        /// <returns>true si se envia false sino</returns>
        public Task<bool> SendEmailSinAutenticacion(string host, int port, string emailRemitente, string emailReceptor, string asunto, string cuerpoEmail);

        /// <summary>
        /// Envia correo utilizando un host que necesite Autenticacion
        /// </summary>
        /// <param name="host">host del smpt</param>
        /// <param name="port">puerto del smpt</param>
        /// <param name="emailRemitente">correo de quien envia</param>
        /// <param name="passwordEmailRemitente">password del correo del que envia</param>
        /// <param name="emailReceptor">correo de quien recibe</param>
        /// <param name="asunto">asunto del correo</param>
        /// <param name="cuerpoEmail">cuerpo o mensaje que se desea enviar</param>
        /// <returns>true si se envia false sino</returns>
        public Task<bool> SendEmailConAutenticacion(string host, int port, string emailRemitente, string passwordEmailRemitente, string emailReceptor, string asunto, string cuerpoEmail);
    }
}
