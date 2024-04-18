using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;

namespace Negocios.Pedido
{
    public class ObtenerCompraPorIdPedido : IRequest<List<DetalleCompraDto>>
    {
        public Guid id { get; set; }
    }

    public class ObtenerCompraPorIdPedidoHandler : IRequestHandler<ObtenerCompraPorIdPedido, List<DetalleCompraDto>>
    {
        private readonly IDataListInformacionCompra _data;
        public ObtenerCompraPorIdPedidoHandler(IDataListInformacionCompra data)
        {
            _data = data;
        }
        public async Task<List<DetalleCompraDto>> Handle(ObtenerCompraPorIdPedido request, CancellationToken cancellationToken)
        {
            return await _data.listarComprasPorIdPedido(request.id);
        }
    }
}
