using Dapper;
using Datos.Interfaces;
using Microsoft.Extensions.Configuration;
using Modelos.Dtos;
using Modelos.Response;
using System.Data;
using System.Data.SqlClient;

namespace Datos.Implementacion
{
    public class DataUsuario : IData<ResponseModel, UsuarioDto>, IDataUsuario,IDataList<UsuarioDto>
    {
        #region VARIABLES 
        private readonly IConfiguration _configuration;
        private string cadenaConexion;
        #endregion

        #region CONSTRUCTOR
        public DataUsuario(IConfiguration configuration)
        {
            _configuration = configuration;
            cadenaConexion = _configuration.GetConnectionString("conexionBDSqlServer")!;
        }
        #endregion

        public Task<ResponseModel> actualizar(UsuarioDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> eliminar(Guid id)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@idUsuario", id);
                    var categoria = await connection.ExecuteAsync("SP_ELIMINAR_USUARIO", parameters, commandType: CommandType.StoredProcedure);
                    return new ResponseModel { ok = true, msg = "Se elimino el Usuario", codStatus = 200 };
                }
            }
            catch (Exception e)
            {
                return new ResponseModel { ok = false, msg = e.Message.ToString(), codStatus = 404 };
            }
        }

        public async Task<ResponseModel> insertar(UsuarioDto model)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    //defino los parametros que recibira el sp de inseratr categoria
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@nombreUsuario", value: model.nombreUsuario, dbType: DbType.String);
                    parameters.Add("@apellidoUsuario", value: model.apellidoUsuario, dbType: DbType.String);
                    parameters.Add("@nidUsuario", value: model.nidUsuario, dbType: DbType.String);
                    parameters.Add("@userName", value: model.userName, dbType: DbType.String);
                    parameters.Add("@password", value: model.password, dbType: DbType.String);
                    parameters.Add("@correoUsuario", value: model.correoUsuario, dbType: DbType.String);
                    parameters.Add("@rolUsuario", value: model.rolUsuario, dbType: DbType.String);

                    //mando a ejecutar el sp
                    await connection.ExecuteAsync("SP_INSERTAR_USUARIO", parameters, commandType: CommandType.StoredProcedure);

                    return new ResponseModel { ok = true, msg = "Usuario registrado", codStatus = 200 };
                }
            }
            catch (Exception)
            {
                return new ResponseModel { ok = false, msg = "Error registrando el Usuario", codStatus = 500 };
            }
        }

        public async Task<List<UsuarioDto>> listar()
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    var usuarios = await connection.QueryAsync<UsuarioDto>("SP_LISTAR_USUARIO", commandType: CommandType.StoredProcedure);
                    return usuarios.ToList();
                }
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task<UsuarioDto> obtenerPorId(Guid id)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@idUsuario", id);
                    var usuario = await connection.QuerySingleAsync<UsuarioDto>("SP_LISTAR_USUARIO", parameters, commandType: CommandType.StoredProcedure);
                    return usuario;
                }
            }
            catch (Exception)
            {

                return null!;
            }
        }


        public async Task<UsuarioDto> obtenerUsuarioPorEmailUsername(string correoOrUserName)
        {
            try
            {
                using (var connection = new SqlConnection(cadenaConexion))
                {
                    //defino los parametros que recibira el sp de inseratr categoria
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@correoOrUserName", value: correoOrUserName, dbType: DbType.String);

                    //mando a ejecutar el sp
                    var usuario = await connection.QuerySingleOrDefaultAsync<UsuarioDto>("SP_OBTENER_USUARIO_POR_EMAIL_USERNAME", parameters, commandType: CommandType.StoredProcedure);

                    return usuario!;
                }
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
