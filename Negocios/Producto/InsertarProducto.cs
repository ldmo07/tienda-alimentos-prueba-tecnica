using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Producto
{
    public class InsertarProducto : IRequest<ResponseModel>
    {
        public ProductoDto modeloEntrada { get; set; }
    }

    public class InsertarCategoriaHandler : IRequestHandler<InsertarProducto, ResponseModel>
    {
        private readonly IData<ResponseModel, ProductoDto> _data;

        public InsertarCategoriaHandler(IData<ResponseModel, ProductoDto> data)
        {
            _data = data;
        }

        public async Task<ResponseModel> Handle(InsertarProducto request, CancellationToken cancellationToken)
        {
            return await _data.insertar(request.modeloEntrada);
        }
    }
}
