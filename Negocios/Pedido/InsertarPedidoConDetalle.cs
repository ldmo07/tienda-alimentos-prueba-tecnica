using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Pedido
{
    public class InsertarPedidoConDetalle : IRequest<ResponseModel>
    {
        public InformacionPedidoDetalleDto modeloEntrada { get; set; }
    }

    public class InsertarPedidoConDetalleHandler : IRequestHandler<InsertarPedidoConDetalle, ResponseModel>
    {
        private readonly IDataInformacionPedidoDetalle _data;

        public InsertarPedidoConDetalleHandler(IDataInformacionPedidoDetalle data)
        {
            _data = data;
        }

        public async Task<ResponseModel> Handle(InsertarPedidoConDetalle request, CancellationToken cancellationToken)
        {
            return await _data.insertar(request.modeloEntrada);
        }
    }
}
