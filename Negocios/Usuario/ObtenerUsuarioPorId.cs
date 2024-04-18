using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Usuario
{
    public class ObtenerUsuarioPorId : IRequest<UsuarioDto>
    {
        public Guid id { get; set; }
    }

    public class ObtenerUsuarioPorIdHandler : IRequestHandler<ObtenerUsuarioPorId, UsuarioDto>
    {
        private readonly IData<ResponseModel,UsuarioDto> _data;

        public ObtenerUsuarioPorIdHandler(IData<ResponseModel, UsuarioDto> data)
        {
            _data = data;
        }
        public async Task<UsuarioDto> Handle(ObtenerUsuarioPorId request, CancellationToken cancellationToken)
        {
            return await _data.obtenerPorId(request.id);
        }
    }
}
