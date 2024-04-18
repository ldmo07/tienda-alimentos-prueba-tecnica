using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;

namespace Negocios.Usuario
{
    public class ListarUsuario : IRequest<List<UsuarioDto>>
    {
    }

    public class ListarUsuarioHandler : IRequestHandler<ListarUsuario, List<UsuarioDto>>
    {
        private readonly IDataList<UsuarioDto> _dataList;
        public ListarUsuarioHandler(IDataList<UsuarioDto> dataList)
        {
            _dataList = dataList;
        }
        public async Task<List<UsuarioDto>> Handle(ListarUsuario request, CancellationToken cancellationToken)
        {
            return await _dataList.listar();
        }
    }
}
