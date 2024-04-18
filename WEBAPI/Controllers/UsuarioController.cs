using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Dtos;
using Modelos.Response;
using Negocios.Usuario;
using System.Net;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        #region VARIABLES
        private readonly IMediator _mediator;
        #endregion

        #region CONSTRUCTOR
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        [HttpPost]
        [Route("insertarUsuario")]
        public async Task<IActionResult> insertarUsuario(UsuarioDto model)
        {
            ResponseModel respuesta = await _mediator.Send(new InsertarUsuario { modeloEntrada = model });
            if (respuesta.ok)
            {
                return Ok(new
                {
                    ok = respuesta.ok,
                    msg = respuesta.msg,
                    token = respuesta.token,
                    rol = model.rolUsuario,
                    email = model.correoUsuario,
                    userName = model.userName
                });
            }
            return BadRequest(respuesta);
        }
        [HttpGet]
        [Route("listarUsuario")]
        public async Task<IActionResult> listarUsuario()
        {
            var respuesta = await _mediator.Send(new ListarUsuario());
            return Ok(respuesta);
        }

        [HttpGet("obtenerUsuarioPorId/{id}")]
        public async Task<IActionResult> obtenerUsuarioPorId(Guid id)
        {
            var respuesta = await _mediator.Send(new ObtenerUsuarioPorId { id = id });
            if (respuesta == null)
            {
                return BadRequest(new ResponseModel { ok = false, msg = "No se encontro un Usuario con ese id", codStatus = ((int)HttpStatusCode.BadRequest) });
            }
            return Ok(respuesta);
        }

        [HttpDelete("eliminarUsuario/{id}")]
        [Authorize]
        public async Task<IActionResult> eliminarUsuario(Guid id)
        {
            var respuesta = await _mediator.Send(new EliminarUsuario { id = id });
            if (!respuesta.ok)
            {
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }
    }
}
