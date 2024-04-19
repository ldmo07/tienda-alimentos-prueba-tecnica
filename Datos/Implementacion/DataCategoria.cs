using Dapper;
using Datos.Interfaces;
using Microsoft.Extensions.Configuration;
using Modelos.Dtos;
using Modelos.Response;
using System.Data;
using System.Data.SqlClient;

namespace Datos.Implementacion
{
    public class DataCategoria : IData<ResponseModel, CategoriaDto>
    {
        #region VARIABLES 
        private readonly IConfiguration _configuration;
        private string cadenaConexion;
        #endregion

        #region CONSTRUCTOR
        public DataCategoria(IConfiguration configuration)
        {
            _configuration = configuration;
            cadenaConexion = _configuration.GetConnectionString("conexionBDSqlServer")!;
        }

        public async Task<ResponseModel> actualizar(CategoriaDto model)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@idCategoria", model.idCategoria);
                    parameters.Add("@nombreCategoria", model.nombreCategoria);
                    parameters.Add("@descripcionCategoria", model.descripcionCategoria);
                    var categoria = await connection.ExecuteAsync("SP_ACTUALIZAR_CATEGORIA", parameters, commandType: CommandType.StoredProcedure);
                    return new ResponseModel { ok = true, msg = "Se Actualizo la Categoria", codStatus = 200 };
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
                    parameters.Add("@idCategoria", id);
                    var categoria = await connection.ExecuteAsync("SP_ELIMINAR_CATEGORIA", parameters, commandType: CommandType.StoredProcedure);
                    return new ResponseModel { ok = true,msg="Se elimino la Categoria", codStatus=200};
                }
            }
            catch (Exception e)
            {
                return new ResponseModel { ok = false, msg = e.Message.ToString(), codStatus = 404 };
            }
        }

        /// <summary>
        /// Este metodo se encarga de registrar una categoria ejecutando el SP SP_INSERTAR_CATEGORIA
        /// </summary>
        /// <param name="model">Recibo un modelo de tipo CategoriaModel</param>
        /// <returns>Retorna un modelo de tipo ResponseModel</returns>
        public async Task<ResponseModel> insertar(CategoriaDto model)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    //defino los parametros que recibira el sp de insertar la categoria
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@nombreCategoria", value: model.nombreCategoria, dbType: DbType.String);
                    parameters.Add("@descripcionCategoria", value: model.descripcionCategoria, dbType: DbType.String);

                    //mando a ejecutar el sp
                    await connection.ExecuteAsync("SP_INSERTAR_CATEGORIA", parameters, commandType: CommandType.StoredProcedure);

                    return new ResponseModel { ok = true, msg = "Categoria registrada", codStatus = 200 };
                }
            }
            catch (Exception)
            {
                return new ResponseModel { ok = false, msg = "Error registrando la categoria", codStatus = 500 };
            }
        }

        public async Task<List<CategoriaDto>> listar()
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    var categorias = await connection.QueryAsync<CategoriaDto>("SP_LISTAR_CATEGORIA", commandType: CommandType.StoredProcedure);
                    return categorias.ToList();
                }
            }
            catch (Exception)
            {

                return null!;
            }
            
        }

        public async Task<CategoriaDto> obtenerPorId(Guid id)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@idCategoria", id);
                    var categoria = await connection.QuerySingleAsync<CategoriaDto>("SP_LISTAR_CATEGORIA", parameters, commandType: CommandType.StoredProcedure);
                    return categoria;
                }
            }
            catch (Exception)
            {

                return null!;
            }
        }
        #endregion


    }
}
