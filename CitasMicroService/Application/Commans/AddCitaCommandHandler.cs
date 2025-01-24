using CitasMicroService.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using CitasMicroService.Domain.Entities;

namespace CitasMicroService.Application.Commans
{
    public class AddCitaCommandHandler : IRequestHandler<AddCitaCommand,int>
    {
        private readonly ICitaRepository _repository;
        public AddCitaCommandHandler(ICitaRepository repository)
        {
            _repository = repository;
        }
        public Task<int> Handle(AddCitaCommand request, CancellationToken cancellationToken)
        {
            Cita cita = new Cita
            {
                codigoPaciente = request.codigoPaciente,
                nombrePaciente = request.nombrePaciente,
                codigoMedico = request.codigoMedico,
                nombreMedico = request.nombreMedico,
                fecha = request.fecha,
                lugar = request.lugar,
                estado = request.estado,
            };
            int result = _repository.Add(cita);
            return Task.FromResult(result);
        }
    }
}