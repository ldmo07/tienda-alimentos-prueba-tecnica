using Dapper;
using Datos.Interfaces;
using Microsoft.Extensions.Configuration;
using Modelos.Dtos;
using Modelos.Response;
using System.Data;
using System.Data.SqlClient;

namespace Datos.Implementacion
{
    public class DataInformacionPedidoDetalle : IDataInformacionPedidoDetalle
    {
        #region VARIABLES 
        private readonly IConfiguration _configuration;
        private string cadenaConexion;
        #endregion

        #region CONSTRUCTOR
        public DataInformacionPedidoDetalle(IConfiguration configuration)
        {
            _configuration = configuration;
            cadenaConexion = _configuration.GetConnectionString("conexionBDSqlServer")!;
        }
        #endregion

        public async Task<ResponseModel> insertar(InformacionPedidoDetalleDto model)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    connection.Open();

                    //inicio la transaccion
                    using (var transaction = connection.BeginTransaction())
                    {
                        //defino esta variable de salida para almacenar el ultimo id del ultimo pedido del Usuario
                        Guid idUltimoPedidoUsuario;

                        try
                        {
                            //defino los parametros que recibira el sp de insertar el pedido
                            DynamicParameters parameters = new DynamicParameters();
                            parameters.Add("@idUsuario", value: model.pedido.idUsuario, dbType: DbType.Guid);
                            parameters.Add("@direccionEnvio", value: model.pedido.direccionEnvio, dbType: DbType.String);
                            parameters.Add("@idUltimoPedidoUsuario", value: null, dbType: DbType.Guid, direction: ParameterDirection.Output, 250);

                            //mando a ejecutar el sp
                            await connection.ExecuteAsync("SP_INSERTAR_PEDIDO", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                            //capturo el id del ultimo pedido del cliente
                            idUltimoPedidoUsuario = parameters.Get<Guid>("@idUltimoPedidoUsuario");
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            return new ResponseModel { ok = false, msg = "Error registrando el pedido", codStatus = 500 };
                        }


                        try
                        {
                            foreach (var detalle in model.detallesPedido)
                            {
                                //defino los parametros que recibe el sp para insertar actualizar las unidades disponibles
                                DynamicParameters parameters2 = new DynamicParameters();
                                parameters2.Add("@idProducto", value: detalle.idProducto, dbType: DbType.Guid);
                                parameters2.Add("@unidadesRestar", value: detalle.unidadesPedidas, dbType: DbType.Int16);
                                //mando a ejecutar el sp
                                await connection.ExecuteAsync("SP_ACTUALIZAR_UNIDADES_DISPONIBLE_PRODUCTO", parameters2, commandType: CommandType.StoredProcedure, transaction: transaction);
                            }
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            return new ResponseModel { ok = false, msg = $"Error actualizando unidades disponibles. {e.Message.ToString()}", codStatus = 500 };
                        }

                        try
                        {
                            foreach (var detalle in model.detallesPedido)
                            {
                                //defino los parametros que recibe el sp para insertar los detalles de pedido
                                DynamicParameters parameters2 = new DynamicParameters();
                                parameters2.Add("@idPedido", value: idUltimoPedidoUsuario, dbType: DbType.Guid);
                                parameters2.Add("@idProducto", value: detalle.idProducto, dbType: DbType.Guid);
                                parameters2.Add("@unidadesPedidas", value: detalle.unidadesPedidas, dbType: DbType.Int16);
                                //mando a ejecutar el sp
                                await connection.ExecuteAsync("SP_INSERTAR_DETALLE_PEDIDO", parameters2, commandType: CommandType.StoredProcedure, transaction: transaction);
                            }

                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            return new ResponseModel { ok = false, msg = "Error registrando el detalle del pedido", codStatus = 500 };
                        }

                        //confirmo la transaccion
                        transaction.Commit();

                        return new ResponseModel { ok = true, msg = "pedido y detalles registrados", codStatus = 200 };
                    }
                }
            }
            catch (Exception)
            {
                return new ResponseModel { ok = false, msg = "Error registrando el pedido y sus detalles", codStatus = 500 };
            }
        }
    }
}
