using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;

namespace Negocios.Pedido
{
    public class ObtenerCompraPorIdUsuario : IRequest<List<DetalleCompraDto>>
    {
        public Guid id { get; set; }
    }

    public class ObtenerCompraPorIdUsuarioHandler : IRequestHandler<ObtenerCompraPorIdUsuario, List<DetalleCompraDto>>
    {
        private readonly IDataListInformacionCompra _data;
        public ObtenerCompraPorIdUsuarioHandler(IDataListInformacionCompra data)
        {
            _data = data;
        }
        public async Task<List<DetalleCompraDto>> Handle(ObtenerCompraPorIdUsuario request, CancellationToken cancellationToken)
        {
            return await _data.listarComprasPorIdUsuario(request.id);
        }
    }
}
