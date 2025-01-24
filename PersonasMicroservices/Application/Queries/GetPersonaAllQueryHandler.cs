using MediatR;
using PersonasMicroservices.Domain.Entities;
using PersonasMicroservices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PersonasMicroservices.Application.Queries
{
    public class GetPersonaAllQueryHandler : IRequestHandler<GetPersonaAllQuery, List<Persona>>
    {
        private readonly IPersonaRepository _repository;
        public GetPersonaAllQueryHandler(IPersonaRepository repository)
        {
            _repository = repository;
        }
        public Task<List<Persona>> Handle(GetPersonaAllQuery request, CancellationToken cancellationToken)
        {
            List<Persona> lstPersonas = _repository.GetAll();
            return Task.FromResult(lstPersonas);
        }
    }
}