using Modelos.Dtos;

namespace Datos.Interfaces
{
    public interface IDataUsuario
    {
        public Task<UsuarioDto> obtenerUsuarioPorEmailUsername(string correoOrUserName);
    }
}
