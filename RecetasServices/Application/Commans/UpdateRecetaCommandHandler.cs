using MediatR;
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
    public class UpdateRecetaCommandHandler:IRequestHandler<UpdateRecetaRequest,Unit>
    {
        private readonly IRecetaRepository _repository;
        public UpdateRecetaCommandHandler(IRecetaRepository repository)
        {
            _repository = repository;
        }

        public Task<Unit> Handle(UpdateRecetaRequest request, CancellationToken cancellationToken)
        {
            // Buscar la receta en la base de datos
            Receta receta = _repository.GetById(request.codigo);
            if (receta == null)
            {
                throw new KeyNotFoundException($"No se encontró una receta con el codigo {request.codigo}");
            }

            receta.codigoPaciente = request.Command.codigoPaciente;
            receta.nombrePaciente = request.Command.nombrePaciente;
            receta.codigoMedico = request.Command.codigoMedico;
            receta.fecha = request.Command.fecha;
            receta.observacion = request.Command.observacion;
            receta.codigoCita = request.Command.codigoCita;
            // Actualizar la lista de detalles de la receta

            receta.detalleReceta = request.Command.detalleReceta.Select((s, index) => new DetalleReceta
            {
                codigoReceta = receta.codigo,
                numero = s.numero,
                nombreMedicamento = s.nombreMedicamento,
                dosis = s.dosis,
                frecuencia = s.frecuencia,

            }).ToList();
            _repository.Update(receta);
            return Task.FromResult(Unit.Value); // Retorna un valor vacío (indica éxito)

        }
    }
}