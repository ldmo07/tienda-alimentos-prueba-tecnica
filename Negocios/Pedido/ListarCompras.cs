using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;

namespace Negocios.Pedido
{
    public class ListarCompras : IRequest<List<DetalleCompraDto>>
    {
    }
    public class ListarComprasHandler : IRequestHandler<ListarCompras, List<DetalleCompraDto>>
    {
        private readonly IDataListInformacionCompra _data;
        public ListarComprasHandler(IDataListInformacionCompra data)
        {
            _data = data;
        }
        public async Task<List<DetalleCompraDto>> Handle(ListarCompras request, CancellationToken cancellationToken)
        {
            return await _data.listarCompras();
        }
    }
}
