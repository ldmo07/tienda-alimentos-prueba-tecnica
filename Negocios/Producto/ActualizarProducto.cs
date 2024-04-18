using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Producto
{
    public class ActualizarProducto : IRequest<ResponseModel>
    {
        public ProductoDto modeloEntrada { get; set; }
    }
    public class ActualizarProductoHandler : IRequestHandler<ActualizarProducto, ResponseModel>
    {
        private readonly IData<ResponseModel,ProductoDto> _data;
        public ActualizarProductoHandler(IData<ResponseModel, ProductoDto> data)
        {
            _data = data;
        }
        public async Task<ResponseModel> Handle(ActualizarProducto request, CancellationToken cancellationToken)
        {
            return await _data.actualizar(request.modeloEntrada);
        }
    }

}
