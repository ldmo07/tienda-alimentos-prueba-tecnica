using Helpers.Intrefaces;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos.Dtos;
using Modelos.Response;
using Negocios.Producto;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        #region VARIABLES
        private readonly IMediator _mediator;
        private readonly IJwtGenerador _jwtGenerador;
        #endregion

        #region CONSTRUCTOR
        public ProductoController(IMediator mediator, IJwtGenerador jwtGenerador)
        {
            _mediator = mediator;
            _jwtGenerador = jwtGenerador;
        }
        #endregion

        [HttpPost]
        [Route("insertarProducto")]
        public async Task<IActionResult> insertarProducto(ProductoDto model)
        {
            //capturo el token y valido que sea Admin
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var dataToken = new JwtSecurityToken(accessToken).Claims;
            var tipoUsuario = _jwtGenerador.obtenerInformacionToken(dataToken);
            if (!tipoUsuario.rol!.Equals("admin"))
            {
                return BadRequest(new ResponseModel { ok = false, msg = "Tu rol no es Administrativo", codStatus = ((int)HttpStatusCode.BadRequest) });
            }

            ResponseModel respuesta = await _mediator.Send(new InsertarProducto { modeloEntrada = model });
            if (respuesta.ok)
            {
                return Ok(respuesta);
            }
            return BadRequest(respuesta);
        }

        [HttpGet]
        [Route("listarProducto")]
        public async Task<IActionResult> listarProducto()
        {
            var respuesta = await _mediator.Send(new ListarProducto());
            return Ok(respuesta);
        }

        [HttpGet("obtenerProductoPorId/{id}")]
        public async Task<IActionResult> obtenerProductoPorId(Guid id)
        {
            var respuesta = await _mediator.Send(new ObtenerProductoPorId { id = id });
            if (respuesta == null)
            {
                return BadRequest(new ResponseModel { ok = false, msg = "No se encontro un Producto con ese id", codStatus = ((int)HttpStatusCode.BadRequest) });
            }
            return Ok(respuesta);
        }

        [HttpDelete("eliminarProducto/{id}")]
        public async Task<IActionResult> eliminarProducto(Guid id)
        {
            //capturo el token y valido que sea Admin
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var dataToken = new JwtSecurityToken(accessToken).Claims;
            var tipoUsuario = _jwtGenerador.obtenerInformacionToken(dataToken);
            if (!tipoUsuario.rol!.Equals("admin"))
            {
                return BadRequest(new ResponseModel { ok = false, msg = "Tu rol no es Administrativo", codStatus = ((int)HttpStatusCode.BadRequest) });
            }

            var respuesta = await _mediator.Send(new EliminarProducto { id = id });
            if (!respuesta.ok)
            {
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }
        [HttpPut("actualizarProducto/{id}")]
        public async Task<IActionResult> actualizarProducto(Guid id, ProductoDto modelo)
        {
            //capturo el token y valido que sea Admin
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var dataToken = new JwtSecurityToken(accessToken).Claims;
            var tipoUsuario = _jwtGenerador.obtenerInformacionToken(dataToken);
            if (!tipoUsuario.rol!.Equals("admin"))
            {
                return BadRequest(new ResponseModel { ok = false, msg = "Tu rol no es Administrativo", codStatus = ((int)HttpStatusCode.BadRequest) });
            }

            modelo.idProducto = id;
            var respuesta = await _mediator.Send(new ActualizarProducto { modeloEntrada = modelo });
            if (!respuesta.ok)
            {
                return BadRequest(respuesta);
            }
            return Ok(respuesta);
        }

    }
}
