using Datos.Interfaces;
using Modelos.Dtos;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace Datos.Implementacion
{
    public class DataListInformacionCompra : IDataListInformacionCompra
    {
        #region VARIABLES 
        private readonly IConfiguration _configuration;
        private string cadenaConexion;
        #endregion

        #region CONSTRUCTOR
        public DataListInformacionCompra(IConfiguration configuration)
        {
            _configuration = configuration;
            cadenaConexion = _configuration.GetConnectionString("conexionBDSqlServer")!;
        }
        #endregion

        public async Task<List<DetalleCompraDto>> listarCompras()
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    var compra = await connection.QueryAsync<DetalleCompraDto>("SP_LISTAR_DETALLE_PEDIDO", commandType: CommandType.StoredProcedure);
                    return compra.ToList();
                }
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task<List<DetalleCompraDto>> listarComprasPorIdPedido(Guid idPedido)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@idPedido", idPedido);
                    var compra = await connection.QueryAsync<DetalleCompraDto>("SP_LISTAR_DETALLE_PEDIDO_POR_ID_PEDIDO",parameters, commandType: CommandType.StoredProcedure);
                    return compra.ToList();
                }
            }
            catch (Exception)
            {

                return null!;
            }
            
        }

        public async Task<List<DetalleCompraDto>> listarComprasPorIdUsuario(Guid idUsuario)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@idUsuario", idUsuario);
                    var compra = await connection.QueryAsync<DetalleCompraDto>("SP_LISTAR_DETALLE_PEDIDO_POR_ID_USUARIO", parameters, commandType: CommandType.StoredProcedure);
                    return compra.ToList();
                }
            }
            catch (Exception)
            {

                return null!;
            }
        }
    }
}
