using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Producto
{
    public class ObtenerProductoPorId : IRequest<ProductoDto>
    {
        public Guid id { get; set; }
    }

    public class ObtenerProductoPorIdHandler : IRequestHandler<ObtenerProductoPorId, ProductoDto>
    {
        private readonly IData<ResponseModel, ProductoDto> _data;
        public ObtenerProductoPorIdHandler(IData<ResponseModel, ProductoDto> data)
        {
            _data = data;
        }
        public Task<ProductoDto> Handle(ObtenerProductoPorId request, CancellationToken cancellationToken)
        {
            return _data.obtenerPorId(request.id);
        }
    }
}
