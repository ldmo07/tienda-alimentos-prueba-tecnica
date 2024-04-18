using Modelos.Dtos;

namespace Modelos.Response
{
    public class ResponseModel
    {
        public bool ok { get; set; }
        public string msg { get; set; }

        public int codStatus { get; set; }
        public string? token { get; set; }

        public static implicit operator ResponseModel(List<CategoriaDto> v)
        {
            throw new NotImplementedException();
        }
    }
}
