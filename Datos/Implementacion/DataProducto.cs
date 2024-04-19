using Dapper;
using Datos.Interfaces;
using Microsoft.Extensions.Configuration;
using Modelos.Dtos;
using Modelos.Response;
using System.Data;
using System.Data.SqlClient;

namespace Datos.Implementacion
{
    public class DataProducto : IData<ResponseModel, ProductoDto>
    {
        #region VARIABLES 
        private readonly IConfiguration _configuration;
        private string cadenaConexion;
        #endregion

        #region CONSTRUCTOR
        public DataProducto(IConfiguration configuration)
        {
            _configuration = configuration;
            cadenaConexion = _configuration.GetConnectionString("conexionBDSqlServer")!;
        }
        #endregion

        public async Task<ResponseModel> actualizar(ProductoDto model)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@idProducto", model.idProducto);
                    parameters.Add("@idCategoria", model.idCategoria);
                    parameters.Add("@nombreProducto", model.nombreProducto);
                    parameters.Add("@precioProducto", model.precioProducto);
                    parameters.Add("@unidadesDisponibles", model.unidadesDisponibles);
                    parameters.Add("@isDisponible", model.isDisponible);
                    var categoria = await connection.ExecuteAsync("SP_ACTUALIZAR_PRODUCTO ", parameters, commandType: CommandType.StoredProcedure);
                    return new ResponseModel { ok = true, msg = "Se actualizo el producto", codStatus = 200 };
                }
            }
            catch (Exception e)
            {
                return new ResponseModel { ok = false, msg = e.Message.ToString(), codStatus = 404 };
            }
        }

        public async Task<ResponseModel> eliminar(Guid id)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@idProducto", id);
                    var categoria = await connection.ExecuteAsync("SP_ELIMINAR_PRODUCTO", parameters, commandType: CommandType.StoredProcedure);
                    return new ResponseModel { ok = true, msg = "Se elimino el producto", codStatus = 200 };
                }
            }
            catch (Exception e)
            {
                return new ResponseModel { ok = false, msg = e.Message.ToString(), codStatus = 404 };
            }
        }

        /// <summary>
        /// Este metodo se encarga de registrar un producto ejecutando el SP SP_INSERTAR_PRODUCTO
        /// </summary>
        /// <param name="model">Recibe un modelo de tipo ProductoDto</param>
        /// <returns>Retorna un modelo de tipo ResponseModel</returns>
        public async Task<ResponseModel> insertar(ProductoDto model)
        {
            
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    //defino los parametros que recibira el sp de inseratr categoria
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@idCategoria", value: model.idCategoria, dbType: DbType.Guid);
                    parameters.Add("@nombreProducto", value: model.nombreProducto, dbType: DbType.String);
                    parameters.Add("@precioProducto", value: model.precioProducto, dbType: DbType.Double);
                    parameters.Add("@unidadesDisponibles", value: model.unidadesDisponibles, dbType: DbType.Int16);
                    parameters.Add("@idUltimoProductoInsertado", value: null, dbType: DbType.Guid, direction: ParameterDirection.Output, 250);

                    //mando a ejecutar el sp
                    await connection.ExecuteAsync("SP_INSERTAR_PRODUCTO", parameters, commandType: CommandType.StoredProcedure);

                    //capturo el id del ultimo producto insertado
                    var idUltimoProductoInsertado = parameters.Get<Guid>("@idUltimoProductoInsertado");

                    return new ResponseModel { ok = true, msg = "Producto registrado", codStatus = 200 };
                }
            }
            catch (Exception)
            {
                return new ResponseModel { ok = false, msg = "Error registrando el producto", codStatus = 500 };
            }


        }

        public async Task<List<ProductoDto>> listar()
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    var productos = await connection.QueryAsync<ProductoDto>("SP_LISTAR_PRODUCTO", commandType: CommandType.StoredProcedure);
                    return productos.ToList();
                }
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task<ProductoDto> obtenerPorId(Guid id)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@idProducto", id);
                    var producto = await connection.QuerySingleAsync<ProductoDto>("SP_LISTAR_PRODUCTO", parameters ,commandType: CommandType.StoredProcedure);
                    return producto;
                }
            }
            catch (Exception)
            {

                return null!;
            }
        }
    }
}
