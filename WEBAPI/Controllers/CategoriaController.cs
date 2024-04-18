using Helpers.Intrefaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Dtos;
using Modelos.Response;
using Negocios.Categoria;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        #region VARIABLES
        private readonly IMediator _mediator;
        private readonly IJwtGenerador _jwtGenerador;
        #endregion

        #region CONSTRUCTOR
        public CategoriaController(IMediator mediator, IJwtGenerador jwtGenerador)
        {
            _mediator = mediator;
            _jwtGenerador = jwtGenerador;
        }
        #endregion

        [HttpPost]
        [Route("insertarCategoria")]
        public async Task<IActionResult> insertarCategoria(CategoriaDto model)
        {
            //capturo el token y valido que sea Admin
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var dataToken = new JwtSecurityToken(accessToken).Claims;
            var tipoUsuario = _jwtGenerador.obtenerInformacionToken(dataToken);
            if (!tipoUsuario.rol!.Equals("admin"))
            {
                return BadRequest(new ResponseModel { ok = false, msg = "Tu rol no es Administrativo", codStatus = ((int)HttpStatusCode.BadRequest) });
            }

            ResponseModel respuesta = await _mediator.Send(new InsertarCategoria { modeloEntrada = model });
            if (respuesta.ok)
            {
                return Ok(respuesta);
            }
            return BadRequest(respuesta);
        }

        [HttpGet]
        [Route("listarCategoria")]
        public async Task<IActionResult> listarCategoria()
        {
            var respuesta = await _mediator.Send(new ListarCategoria());
            return Ok(respuesta);
        }

        [HttpGet("obtenerCategoriaPorId/{id}")]
        public async Task<IActionResult> obtenerCategoriaPorId(Guid id)
        {
            var respuesta = await _mediator.Send(new ObtenerCategoriaPorId { id = id });
            if (respuesta == null)
            {
                return BadRequest(new ResponseModel { ok = false, msg = "No se encontro una Categoria con ese id", codStatus = ((int)HttpStatusCode.BadRequest) });
            }
            return Ok(respuesta);
        }

        [HttpDelete("eliminarCategoria/{id}")]
        public async Task<IActionResult> eliminarCategoria(Guid id)
        {
            //capturo el token y valido que sea Admin
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var dataToken = new JwtSecurityToken(accessToken).Claims;
            var tipoUsuario = _jwtGenerador.obtenerInformacionToken(dataToken);
            if (!tipoUsuario.rol!.Equals("admin"))
            {
                return BadRequest(new ResponseModel { ok = false, msg = "Tu rol no es Administrativo", codStatus = ((int)HttpStatusCode.BadRequest) });
            }

            var respuesta = await _mediator.Send(new EliminarCategoria { id = id });
            if (!respuesta.ok)
            {
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }

        [HttpPut("actualizarCategoria/{id}")]
        public async Task<IActionResult> actualizarCategoria(Guid id, CategoriaDto modelo)
        {
            //capturo el token y valido que sea Admin
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var dataToken = new JwtSecurityToken(accessToken).Claims;
            var tipoUsuario = _jwtGenerador.obtenerInformacionToken(dataToken);
            if (!tipoUsuario.rol!.Equals("admin"))
            {
                return BadRequest(new ResponseModel { ok = false, msg = "Tu rol no es Administrativo", codStatus = ((int)HttpStatusCode.BadRequest) });
            }

            modelo.idCategoria = id;
            var respuesta = await _mediator.Send(new ActualizarCategoria { modeloEntrada = modelo });
            if (!respuesta.ok)
            {
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }
    }
}
