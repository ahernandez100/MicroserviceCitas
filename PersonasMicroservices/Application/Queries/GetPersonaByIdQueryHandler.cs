using MediatR;
using PersonasMicroservices.Domain.Entities;
using PersonasMicroservices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using PersonasMicroservices.Api.Exeptions;

namespace PersonasMicroservices.Application.Queries
{
    public class GetPersonaByIdQueryHandler : IRequestHandler<GetPersonaByIdQuery, Persona>
    {
        private readonly IPersonaRepository _repository;

        public GetPersonaByIdQueryHandler(IPersonaRepository repository)
        {
            _repository = repository;
        }

        public Task<Persona> Handle(GetPersonaByIdQuery request, CancellationToken cancellationToken)
        {

            Persona persona = _repository.GetById(request.codigo);
            if (persona == null)
            {
                throw new NotFoundException($"Persona con codigo {request.codigo} no encontrada.");
            }
            return Task.FromResult(persona);
        }
    }
}