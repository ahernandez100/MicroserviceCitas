using CitasMicroService.Api.Exeptions;
using CitasMicroService.Domain.Entities;
using CitasMicroService.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CitasMicroService.Application.Commans
{
    public class DeleteCitaCommandHandler:IRequestHandler<DeleteCitaCommand,int>
    {
        private readonly ICitaRepository _repository;
        public DeleteCitaCommandHandler(ICitaRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(DeleteCitaCommand request, CancellationToken cancellationToken)
        {
            Cita cita = _repository.GetById(request.codigo);
            if (cita == null)
            {
                throw new NotFoundException($"Persona con codigo {request.codigo} no encontrada.");
            }
            int result = _repository.Delete(request.codigo);
            return Task.FromResult(result);
        }
    }
    
}