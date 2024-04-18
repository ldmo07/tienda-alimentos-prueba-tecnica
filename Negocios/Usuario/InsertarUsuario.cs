using Datos.Interfaces;
using Helpers.Intrefaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Usuario
{
    public class InsertarUsuario : IRequest<ResponseModel>
    {
        public UsuarioDto modeloEntrada { get; set; }
    }
    public class InsertarUsuarioHandler : IRequestHandler<InsertarUsuario, ResponseModel>
    {
        private readonly IData<ResponseModel, UsuarioDto> _data;
        private readonly IJwtGenerador _jwtGenerador;
        private readonly IBecryptHelper _becryptHelper;

        public InsertarUsuarioHandler(IData<ResponseModel, UsuarioDto> data, IJwtGenerador jwtGenerador, IBecryptHelper becryptHelper)
        {
            _data = data;
            _jwtGenerador = jwtGenerador;
            _becryptHelper = becryptHelper;
        }
        public async Task<ResponseModel> Handle(InsertarUsuario request, CancellationToken cancellationToken)
        {
            //encripto el password del usuario
            request.modeloEntrada.password = _becryptHelper.encrypt(request.modeloEntrada.password);

            var resp = await _data.insertar(request.modeloEntrada);
            if (resp.ok)
            {
                resp.token = _jwtGenerador.crearToken(
               new UsuarioDto
               {
                   userName = request.modeloEntrada.userName,
                   rolUsuario = request.modeloEntrada.rolUsuario,
                   correoUsuario = request.modeloEntrada.correoUsuario
               });
            }
            return resp;
        }
    }
}
