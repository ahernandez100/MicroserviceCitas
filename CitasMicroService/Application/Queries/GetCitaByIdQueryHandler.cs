using CitasMicroService.Domain.Entities;
using CitasMicroService.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using CitasMicroService.Api.Exeptions;

namespace CitasMicroService.Application.Queries
{
    public class GetCitaByIdQueryHandler: IRequestHandler<GetCitaByIdQuery,Cita>
    {
        private readonly ICitaRepository _repository;
        public GetCitaByIdQueryHandler(ICitaRepository repository)
        {
            _repository = repository;
        }
        public Task<Cita> Handle(GetCitaByIdQuery request, CancellationToken cancellationToken)
        {

            Cita cita = _repository.GetById(request.codigo);
            if (cita == null)
            {
                throw new NotFoundException($"Cita con codigo {request.codigo} no encontrada.");
            }
            return Task.FromResult(cita);
        }
    }
}