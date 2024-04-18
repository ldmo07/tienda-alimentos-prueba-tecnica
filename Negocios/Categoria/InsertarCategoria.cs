using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Categoria
{
    public class InsertarCategoria : IRequest<ResponseModel>
    {
        public CategoriaDto modeloEntrada { get; set; }
    }

    public class InsertarCategoriaHandler : IRequestHandler<InsertarCategoria, ResponseModel>
    {
        private readonly IData<ResponseModel, CategoriaDto> _data;

        public InsertarCategoriaHandler(IData<ResponseModel, CategoriaDto> data)
        {
            _data = data;
        }

        public async Task<ResponseModel> Handle(InsertarCategoria request, CancellationToken cancellationToken)
        {
            return await _data.insertar(request.modeloEntrada);
        }
    }
}
