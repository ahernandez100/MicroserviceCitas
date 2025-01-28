using MediatR;
using RecetasServices.Domain.Entities;
using RecetasServices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using RecetasServices.Api.Exeptions;

namespace RecetasServices.Application.Queries
{
    public class GetRecetaByIdPacienteQueryHandler : IRequestHandler<GetRecetaByIdPacienteQuery, Receta>
    {
        private readonly IRecetaRepository _repository;
        public GetRecetaByIdPacienteQueryHandler(IRecetaRepository repository)
        {
            _repository = repository;
        }
        public Task<Receta> Handle(GetRecetaByIdPacienteQuery request, CancellationToken cancellationToken)
        {

            Receta receta = _repository.GetByIdPaciente(request.codigoPaciente);
            if (receta == null)
            {
                throw new NotFoundException($"receta con codigo {request.codigoPaciente} no encontrada.");
            }
            return Task.FromResult(receta);
        }
    }
}