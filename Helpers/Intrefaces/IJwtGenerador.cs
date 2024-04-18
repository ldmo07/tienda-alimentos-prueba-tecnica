using Modelos.Dtos;
using Modelos.Utils;
using System.Security.Claims;

namespace Helpers.Intrefaces
{
    public interface IJwtGenerador
    {
        string crearToken(UsuarioDto usuario);
        string validateToken(string token);
        InformacionUsuarioTokenModel obtenerInformacionToken(IEnumerable<Claim> claims);
    }
}
