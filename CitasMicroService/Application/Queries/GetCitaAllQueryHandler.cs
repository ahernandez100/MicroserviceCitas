using CitasMicroService.Domain.Entities;
using CitasMicroService.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CitasMicroService.Application.Queries
{
    public class GetCitaAllQueryHandler : IRequestHandler<GetCitaAllQuery, List<Cita>>
    {
        private readonly ICitaRepository _repository;
        public GetCitaAllQueryHandler(ICitaRepository repository)
        {
            _repository = repository;
        }
        public Task<List<Cita>> Handle(GetCitaAllQuery request, CancellationToken cancellationToken)
        {
            List<Cita> lstCitas = _repository.GetAll();
            return Task.FromResult(lstCitas);
        }
    }
}