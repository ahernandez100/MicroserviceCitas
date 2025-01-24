using MediatR;
using PersonasMicroservices.Api.Exeptions;
using PersonasMicroservices.Domain.Entities;
using PersonasMicroservices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PersonasMicroservices.Application.Commans
{
    public class DeletePersonaCommandHandler: IRequestHandler<DeletePersonaCommand, int>
    {
        private readonly IPersonaRepository _repository;
        public DeletePersonaCommandHandler(IPersonaRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(DeletePersonaCommand request, CancellationToken cancellationToken)
        {
            Persona persona = _repository.GetById(request.codigo);
            if (persona == null)
            {
                throw new NotFoundException($"Persona con codigo {request.codigo} no encontrada.");
            }
            int result = _repository.Delete(request.codigo);
            return Task.FromResult(result);
        }
    }
}