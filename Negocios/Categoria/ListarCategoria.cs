using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Categoria
{
    public class ListarCategoria : IRequest<List<CategoriaDto>>
    {
    }

    public class ListarCategoriaHandler : IRequestHandler<ListarCategoria, List<CategoriaDto>>
    {
        private readonly IDataList<CategoriaDto> _data;
        public ListarCategoriaHandler(IDataList<CategoriaDto> data)
        {
            _data = data;
        }

        public async Task<List<CategoriaDto>> Handle(ListarCategoria request, CancellationToken cancellationToken)
        {
            return await _data.listar();
        }
    }
}
