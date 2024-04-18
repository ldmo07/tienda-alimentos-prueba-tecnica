using Helpers.Intrefaces;

namespace Helpers.Implementacion
{
    public class BecryptHelper : IBecryptHelper
    {
        public string encrypt(string plainText)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainText);
        }

        public bool validarEncrypt(string encriptText, string plainText)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, encriptText);
        }
    }
}
