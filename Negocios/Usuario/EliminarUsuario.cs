using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Usuario
{
    public class EliminarUsuario : IRequest<ResponseModel>
    {
        public Guid id { get; set; }
    }
    public class EliminarUsuarioHandler : IRequestHandler<EliminarUsuario, ResponseModel>
    {
        private readonly IData<ResponseModel, UsuarioDto> _data;
        public EliminarUsuarioHandler(IData<ResponseModel, UsuarioDto> data)
        {
            _data = data;
        }
        public async Task<ResponseModel> Handle(EliminarUsuario request, CancellationToken cancellationToken)
        {
            return await _data.eliminar(request.id);
        }
    }
}
