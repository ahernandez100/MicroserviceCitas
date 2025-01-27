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
    public class GetRecetaByIdQueryHandler:IRequestHandler<GetRecetaByIdQuery,Receta>
    {
        private readonly IRecetaRepository _repository;
        public GetRecetaByIdQueryHandler(IRecetaRepository repository)
        {
            _repository = repository;
        }
        public Task<Receta> Handle(GetRecetaByIdQuery request, CancellationToken cancellationToken)
        {

            Receta receta = _repository.GetById(request.codigo);
            if (receta == null)
            {
                throw new NotFoundException($"receta con codigo {request.codigo} no encontrada.");
            }
            return Task.FromResult(receta);
        }

    }
}