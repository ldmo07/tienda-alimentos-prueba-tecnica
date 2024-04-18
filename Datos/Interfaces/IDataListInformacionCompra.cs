using Modelos.Dtos;

namespace Datos.Interfaces
{
    public interface IDataListInformacionCompra
    {
        Task<List<DetalleCompraDto>> listarCompras();
        Task<List<DetalleCompraDto>> listarComprasPorIdPedido(Guid idPedido);
        Task<List<DetalleCompraDto>> listarComprasPorIdUsuario(Guid idUsuario);
    }
}
