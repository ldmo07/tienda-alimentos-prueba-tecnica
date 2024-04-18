using Helpers.Intrefaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Helpers.Implementacion
{
    public class EmailHelper : IEmailHelper
    {
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
        public async Task<bool> SendEmailConAutenticacion(string host, int port, string emailRemitente, string passwordEmailRemitente, string emailReceptor, string asunto, string cuerpoEmail)
        {
            try
            {
                // create email message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(emailRemitente));
                email.To.Add(MailboxAddress.Parse(emailReceptor));
                email.Subject = asunto;
                email.Body = new TextPart(TextFormat.Html) { Text = cuerpoEmail /*"<h1>Example HTML Message Body</h1>"*/ };

                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
                    await smtp.AuthenticateAsync(emailRemitente, passwordEmailRemitente);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

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
        public async Task<bool> SendEmailSinAutenticacion(string host, int port, string emailRemitente, string emailReceptor, string asunto, string cuerpoEmail)
        {
            try
            {
                // create email message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(emailRemitente));
                email.To.Add(MailboxAddress.Parse(emailReceptor));
                email.Subject = asunto;
                email.Body = new TextPart(TextFormat.Html) { Text = cuerpoEmail /*"<h1>Example HTML Message Body</h1>"*/ };

                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync(host, port, SecureSocketOptions.None);
                    //await smtp.AuthenticateAsync(emailRemitente, passwordEmailRemitente);
                    await smtp.SendAsync(email);
                    await smtp.DisconnectAsync(true);
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
