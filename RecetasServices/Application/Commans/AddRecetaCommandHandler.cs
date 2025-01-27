using MediatR;
using RecetasServices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using RecetasServices.Domain.Entities;
using System.Reflection;

namespace RecetasServices.Application.Commans
{
    public class AddRecetaCommandHandler : IRequestHandler<AddRecetaCommand, int>
    {
        private readonly IRecetaRepository _repository;
        public AddRecetaCommandHandler(IRecetaRepository repository)
        {
            _repository = repository;
        }
        public Task<int> Handle(AddRecetaCommand request, CancellationToken cancellationToken)
        {
            Receta receta = new Receta
            {
                codigoPaciente = request.codigoPaciente,
                nombrePaciente = request.nombrePaciente,
                codigoMedico = request.codigoMedico,
                nombreMedico= request.nombreMedico,
                fecha = request.fecha,
                observacion = request.observacion,
                codigoCita= request.codigoCita,
                detalleReceta = request.detalleReceta.Select((s, index) => new DetalleReceta
                {
                    numero= index + 1, // Calcula el número ascendente comenzando desde 1
                    nombreMedicamento = s.nombreMedicamento,
                    dosis=s.dosis,
                    frecuencia=s.frecuencia,

                }).ToList()
            };
            int result = _repository.Add(receta);
            return Task.FromResult(result);

        }
    }
}