namespace Helpers.Intrefaces
{
    public interface IBecryptHelper
    {
        string encrypt(string plainText);
        bool validarEncrypt(string encriptText, string plainText);

    }
}
