using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Categoria
{
    public class EliminarCategoria : IRequest<ResponseModel>
    {
        public Guid id { get; set; }
    }

    public class EliminarCategoriaHandler : IRequestHandler<EliminarCategoria, ResponseModel>
    {
        private readonly IData<ResponseModel,CategoriaDto> _data;

        public EliminarCategoriaHandler(IData<ResponseModel, CategoriaDto> data)
        {
            _data = data;
        }
        public async Task<ResponseModel> Handle(EliminarCategoria request, CancellationToken cancellationToken)
        {
            return await _data.eliminar(request.id);
        }
    }
}
