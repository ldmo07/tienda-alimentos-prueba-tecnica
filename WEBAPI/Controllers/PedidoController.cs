using Helpers.Intrefaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Modelos.Dtos;
using Modelos.Response;
using Negocios.Pedido;
using Negocios.Usuario;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using WEBAPI.Modelos;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PedidoController : ControllerBase
    {
        #region VARIABLES
        private readonly IMediator _mediator;
        private IEmailHelper _emailHelper;
        private readonly ConfiguracionSmtp _configuracionSmtp;
        private readonly IJwtGenerador _jwtGenerador;
        #endregion

        #region CONSTRUCTOR
        public PedidoController(IMediator mediator, IEmailHelper emailHelper, IOptions<ConfiguracionSmtp> configuracionSmtp, IJwtGenerador jwtGenerador)
        {
            _mediator = mediator;
            _emailHelper = emailHelper;
            _configuracionSmtp = configuracionSmtp.Value;
            _jwtGenerador = jwtGenerador;
        }
        #endregion

        [HttpPost]
        [Route("insertarPedidoConDetalles")]
        public async Task<IActionResult> insertarPedidoConDetalles(InformacionPedidoDetalleDto model)
        {
            ResponseModel respuesta = await _mediator.Send(new InsertarPedidoConDetalle { modeloEntrada = model });
            if (respuesta.ok)
            {
                UsuarioDto usuario = await _mediator.Send(new ObtenerUsuarioPorId { id = model.pedido.idUsuario });
                string asuntoCorreo = "Confirmacion Pedido Tienda de Alimentos Online";
                string cuerpoCorreo = $"<h1>Felicitaciones {usuario.nombreUsuario} Tu pedido se realizo correctamente<h1/>";

                bool isSend = await _emailHelper.SendEmailConAutenticacion(_configuracionSmtp.host, _configuracionSmtp.port, _configuracionSmtp.emailRemitente,
                                _configuracionSmtp.passwordEmailRemitente, usuario.correoUsuario, asuntoCorreo, cuerpoCorreo);

                if (isSend)
                {
                    return Ok(respuesta);
                }

                return Ok(new ResponseModel
                {
                    codStatus = respuesta.codStatus,
                    ok = respuesta.ok,
                    msg = "Se realizo el pedido pero no se pudo enviar el correo de confirmacion",
                });
            }
            return BadRequest(respuesta);
        }

        [HttpGet("listarCompras")]
        public async Task<IActionResult> listarCompras()
        {
            //capturo el token y valido que sea Admin
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var dataToken = new JwtSecurityToken(accessToken).Claims;
            var tipoUsuario = _jwtGenerador.obtenerInformacionToken(dataToken);
            if (!tipoUsuario.rol!.Equals("admin"))
            {
                return BadRequest(new ResponseModel { ok = false, msg = "Tu rol no es Administrativo", codStatus = ((int)HttpStatusCode.BadRequest) });
            }

            var respuesta = await _mediator.Send(new ListarCompras());
            return Ok(respuesta);
        }

        [HttpGet("obtenerCompraPorIdPedido/{idPedido}")]
        public async Task<IActionResult> obtenerCompraPorIdPedido(Guid idPedido)
        {
            //capturo el token y valido que sea Admin
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var dataToken = new JwtSecurityToken(accessToken).Claims;
            var tipoUsuario = _jwtGenerador.obtenerInformacionToken(dataToken);
            if (!tipoUsuario.rol!.Equals("admin"))
            {
                return BadRequest(new ResponseModel { ok = false, msg = "Tu rol no es Administrativo", codStatus = ((int)HttpStatusCode.BadRequest) });
            }

            var respuesta = await _mediator.Send(new ObtenerCompraPorIdPedido { id = idPedido });
            if (respuesta == null || respuesta.Count == 0)
            {
                return BadRequest(new ResponseModel { ok = false, msg = "No se encontraron compras con ese id de peido", codStatus = ((int)HttpStatusCode.BadRequest) });
            }
            return Ok(respuesta);
        }

        [HttpGet("obtenerCompraPorIdUsuario/{idUsuario}")]
        public async Task<IActionResult> obtenerCompraPorIdUsuario(Guid idUsuario)
        {
            //capturo el token y valido que sea Admin
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var dataToken = new JwtSecurityToken(accessToken).Claims;
            var tipoUsuario = _jwtGenerador.obtenerInformacionToken(dataToken);
            if (!tipoUsuario.rol!.Equals("admin"))
            {
                return BadRequest(new ResponseModel { ok = false, msg = "Tu rol no es Administrativo", codStatus = ((int)HttpStatusCode.BadRequest) });
            }

            var respuesta = await _mediator.Send(new ObtenerCompraPorIdUsuario { id = idUsuario });
            if (respuesta == null || respuesta.Count == 0)
            {
                return BadRequest(new ResponseModel { ok = false, msg = "No se encontraron compras con ese id de usuario", codStatus = ((int)HttpStatusCode.BadRequest) });
            }
            return Ok(respuesta);
        }


    }
}

