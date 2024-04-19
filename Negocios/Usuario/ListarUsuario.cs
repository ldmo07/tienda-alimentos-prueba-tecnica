using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Usuario
{
    public class ListarUsuario : IRequest<List<UsuarioDto>>
    {
    }

    public class ListarUsuarioHandler : IRequestHandler<ListarUsuario, List<UsuarioDto>>
    {
        private readonly IData<ResponseModel,UsuarioDto> _dataList;
        public ListarUsuarioHandler(IData<ResponseModel, UsuarioDto> dataList)
        {
            _dataList = dataList;
        }
        public async Task<List<UsuarioDto>> Handle(ListarUsuario request, CancellationToken cancellationToken)
        {
            return await _dataList.listar();
        }
    }
}
