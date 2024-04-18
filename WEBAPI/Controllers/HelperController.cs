using Helpers.Intrefaces;
using Microsoft.AspNetCore.Mvc;
using Modelos.Utils;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelperController : ControllerBase
    {
        private IEmailHelper _emailHelper;
        public HelperController(IEmailHelper emailHelper)
        {
            _emailHelper = emailHelper;
        }

        [HttpPost]
        [Route("enviarCorreoSinAutenticar")]
        public async Task<IActionResult> enviarMsjCorreoSinAutenticar(ParametrosCorreoModel param)
        {

            bool isSend = await _emailHelper.SendEmailSinAutenticacion(param.host, param.port, param.emailRemitente, param.emailReceptor, param.asunto, param.cuerpoEmail);

            if (isSend) return Ok(new { ok = true, mensaje = "Correo enviado con exito" });

            return BadRequest(new { ok = false, mensaje = "Ocurrio un fallo al enviar el correo" });
        }

        [HttpPost]
        [Route("enviarCorreoConAutenticacion")]
        public async Task<IActionResult> enviarCorreoConAutenticacion(ParametrosCorreoModel param)
        {

            bool isSend = await _emailHelper.SendEmailConAutenticacion(param.host, param.port, param.emailRemitente, param.passwordEmailRemitente!, param.emailReceptor, param.asunto, param.cuerpoEmail);

            if (isSend) return Ok(new { ok = true, mensaje = "Correo enviado con exito" });

            return BadRequest(new { ok = false, mensaje = "Ocurrio un fallo al enviar el correo" });

        }
    }
}
