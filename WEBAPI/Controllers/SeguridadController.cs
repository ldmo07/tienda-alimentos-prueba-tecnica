using Helpers.Intrefaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Dtos;
using Modelos.Response;
using Negocios.Usuario;
using System.IdentityModel.Tokens.Jwt;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        #region VARIABLES
        private readonly IJwtGenerador _jwtGenerador;
        private readonly IBecryptHelper _becryptHelper;
        private readonly IMediator _mediator;
        #endregion

        #region CONSTRUCTOR
        public SeguridadController(IJwtGenerador jwtGenerador, IMediator mediator, IBecryptHelper becryptHelper)
        {
            _jwtGenerador = jwtGenerador;
            _mediator = mediator;
            _becryptHelper = becryptHelper;
        }
        #endregion

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login(UsuarioLoginDto usuario)
        {

            ResponseModel respuesta = await _mediator.Send(new LoginUsuario { password = usuario.password, correoOrUserName = usuario.userName });
            if (respuesta.ok)
            {
                //obtengo la informacion del token
                var dataToken = new JwtSecurityToken(respuesta.token).Claims;
                var email = dataToken.Where(x => x.Type.Equals("email")).Select(x => x.Value).FirstOrDefault();
                var rol = dataToken.Where(x => x.Type.Equals("role")).Select(x => x.Value).FirstOrDefault();
                var userName = dataToken.Where(x => x.Type.Equals("sub")).Select(x => x.Value).FirstOrDefault();

                return Ok(new
                {
                    ok = respuesta.ok,
                    msg = respuesta.msg,
                    token = respuesta.token,
                    rol,
                    email,
                    userName
                });
            }
            return BadRequest(respuesta);
        }

        [HttpGet]
        [Authorize]
        [Route("validarToken")]
        public async Task<IActionResult> protegido()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var dataToken = new JwtSecurityToken(accessToken).Claims;

            //valido el token
            var valido = _jwtGenerador.validateToken(accessToken!);
            if (string.IsNullOrEmpty(valido)) return Unauthorized(new { ok = false, msg = "No autorizado" });

            //obtengo la informacion del token
            var email = dataToken.Where(x => x.Type.Equals("email")).Select(x => x.Value).FirstOrDefault();
            var rol = dataToken.Where(x => x.Type.Equals("role")).Select(x => x.Value).FirstOrDefault();
            var userName = dataToken.Where(x => x.Type.Equals("sub")).Select(x => x.Value).FirstOrDefault();

            return Ok(new { ok = true, email, rol, userName });
        }
    }
}
