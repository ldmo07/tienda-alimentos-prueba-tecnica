using Modelos.Dtos;
using Modelos.Response;

namespace Datos.Interfaces
{
    public interface IDataInformacionPedidoDetalle
    {
        public Task<ResponseModel> insertar(InformacionPedidoDetalleDto model);
    }
}
