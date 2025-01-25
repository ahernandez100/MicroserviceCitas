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
    public class UpdateCitaCommandHandler: IRequestHandler<UpdateCitaRequest, Unit>
    {
        private readonly ICitaRepository _repository;
        public UpdateCitaCommandHandler(ICitaRepository repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(UpdateCitaRequest request, CancellationToken cancellationToken)
        {
            //Busca la cita en la base de datos
            Cita cita = _repository.GetById(request.codigo);
            if (cita == null)
            {
                throw new KeyNotFoundException($"No se encontró una cita con el codigo {request.codigo}");
            }
            cita.codigoMedico = request.Commnad.codigoPaciente;
            cita.nombrePaciente = request.Commnad.nombrePaciente;
            cita.codigoMedico=request.Commnad.codigoMedico;
            cita.nombreMedico = request.Commnad.nombreMedico;
            cita.fecha = request.Commnad.fecha;
            cita.lugar = request.Commnad.lugar;
            cita.estado = request.Commnad.estado;
            _repository.Update(cita);
            return Task.FromResult(Unit.Value); // Retorna un valor vacío (indica éxito
        }
    }
}