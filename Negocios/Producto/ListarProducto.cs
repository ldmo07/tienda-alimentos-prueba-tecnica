using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Producto
{
    public class ListarProducto : IRequest<List<ProductoDto>> 
    {
    }

    public class ListarProductoHandler : IRequestHandler<ListarProducto, List<ProductoDto>>
    {
        private readonly IData<ResponseModel,ProductoDto> _dataList;
        public ListarProductoHandler(IData<ResponseModel, ProductoDto> dataList)
        {
                _dataList = dataList;
        }
        public async Task<List<ProductoDto>> Handle(ListarProducto request, CancellationToken cancellationToken)
        {
            return await _dataList.listar();
        }
    }
}
