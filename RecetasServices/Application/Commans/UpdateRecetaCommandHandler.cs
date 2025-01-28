using MediatR;
using RecetasServices.Domain.Entities;
using RecetasServices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RecetasServices.Application.Commans
{
    public class UpdateRecetaCommandHandler:IRequestHandler<UpdateRecetaRequest,Unit>
    {
        private readonly IRecetaRepository _repository;
        public UpdateRecetaCommandHandler(IRecetaRepository repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(UpdateRecetaRequest request, CancellationToken cancellationToken)
        {
            // Buscar la receta en la base de datos
            Receta receta = _repository.GetById(request.codigo);
            if (receta == null)
            {
                throw new KeyNotFoundException($"No se encontró una receta con el codigo {request.codigo}");
            }
            receta.estado = request.Command.estado;
            _repository.Update(receta);
            return Task.FromResult(Unit.Value); // Retorna un valor vacío (indica éxito)

        }
    }
}