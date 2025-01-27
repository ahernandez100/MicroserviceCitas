using MediatR;
using RecetasServices.Api.Exeptions;
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
    public class DeleteRecetaCommandHandler: IRequestHandler<DeleteRecetaCommand,int>
    {
        private readonly IRecetaRepository _repository;
        public DeleteRecetaCommandHandler(IRecetaRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(DeleteRecetaCommand request, CancellationToken cancellationToken)
        {
            Receta receta = _repository.GetById(request.codigo);
            if (receta == null)
            {
                throw new NotFoundException($"Receta con codigo {request.codigo} no encontrada.");
            }
            int result = _repository.Delete(request.codigo);
            return Task.FromResult(result);
        }
    }
}