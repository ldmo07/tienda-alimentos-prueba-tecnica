using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Producto
{
    public class EliminarProducto : IRequest<ResponseModel>
    {
        public Guid id { get; set; }
    }

    public class EliminarProductoHandler : IRequestHandler<EliminarProducto, ResponseModel>
    {
        private readonly IData<ResponseModel, ProductoDto> _data;
        public EliminarProductoHandler(IData<ResponseModel, ProductoDto> data)
        {
            _data = data;
        }
        public async Task<ResponseModel> Handle(EliminarProducto request, CancellationToken cancellationToken)
        {
            return await _data.eliminar(request.id);
        }
    }
}
