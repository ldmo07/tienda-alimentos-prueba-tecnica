using Datos.Interfaces;
using MediatR;
using Modelos.Dtos;
using Modelos.Response;

namespace Negocios.Categoria
{
    public class ActualizarCategoria : IRequest<ResponseModel>
    {
        public CategoriaDto modeloEntrada { get; set; }
    }

    public class ActualizarCategoriaHandler : IRequestHandler<ActualizarCategoria, ResponseModel>
    {
        private readonly IData<ResponseModel,CategoriaDto> _data;
        public ActualizarCategoriaHandler(IData<ResponseModel, CategoriaDto> data)
        {
            _data = data;
        }
        public async Task<ResponseModel> Handle(ActualizarCategoria request, CancellationToken cancellationToken)
        {
            return await _data.actualizar(request.modeloEntrada);
        }
    }
}
