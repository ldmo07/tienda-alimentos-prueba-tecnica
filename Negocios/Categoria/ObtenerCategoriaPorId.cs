using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Categoria
{
    public class ObtenerCategoriaPorId : IRequest<CategoriaDto>
    {
        public Guid id { get; set; }
    }

    public class ObtenerCategoriaPorIdHandler : IRequestHandler<ObtenerCategoriaPorId, CategoriaDto>
    {
        private readonly IData<ResponseModel,CategoriaDto> _data;

        public ObtenerCategoriaPorIdHandler(IData<ResponseModel, CategoriaDto> data)
        {
            _data = data;
        }
        public async Task<CategoriaDto> Handle(ObtenerCategoriaPorId request, CancellationToken cancellationToken)
        {
            return await _data.obtenerPorId(request.id);
        }
    }
}
