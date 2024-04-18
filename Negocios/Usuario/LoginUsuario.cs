using Datos.Interfaces;
using Helpers.Intrefaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;
using System.Net;

namespace Negocios.Usuario
{
    public class LoginUsuario : IRequest<ResponseModel>
    {
        public string correoOrUserName { get; set; }
        public string password { get; set; }

    }

    public class LoginUsuarioHandler : IRequestHandler<LoginUsuario, ResponseModel>
    {
        private readonly IDataUsuario _data;
        private readonly IJwtGenerador _jwtGenerador;
        private readonly IBecryptHelper _becryptHelper;

        public LoginUsuarioHandler(IDataUsuario data, IJwtGenerador jwtGenerador, IBecryptHelper becryptHelper)
        {
            _data = data;
            _jwtGenerador = jwtGenerador;
            _becryptHelper = becryptHelper;
        }

        public async Task<ResponseModel> Handle(LoginUsuario request, CancellationToken cancellationToken)
        {
            UsuarioDto usuario = await _data.obtenerUsuarioPorEmailUsername(request.correoOrUserName);

            if (usuario == null)
            {
                return new ResponseModel { ok = false, msg = "Error al iniciar sesion", codStatus = ((int)HttpStatusCode.BadRequest) };
            }

            //valido si la contraseña que envia el usuario es valida con la que esta en bd
            bool validPassword = _becryptHelper.validarEncrypt(usuario.password, request.password);

            if (validPassword)
            {
                var token = _jwtGenerador.crearToken(usuario);
                return new ResponseModel { ok = true, msg = "usuario logeado", token = token, codStatus = ((int)HttpStatusCode.OK) };
            }

            return new ResponseModel { ok = false, msg = "Error al iniciar sesion", codStatus = ((int)HttpStatusCode.BadRequest) };
        }
    }
}
