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
using RecetasServices.Application.DTO;
using RecetasServices.Application.Services;
using RecetasServices.Api.Exeptions;

namespace RecetasServices.Application.Commans
{
    public class AddRecetaCommandHandler : IRequestHandler<AddRecetaRequest, int>
    {
        private readonly IRecetaRepository _repository;
        private readonly IPersonaService _personaService;
        private readonly ICitaService _citaService;
        public AddRecetaCommandHandler(IRecetaRepository repository, IPersonaService personaService, ICitaService citaService)
        {
            _repository = repository;
            _personaService = personaService;
            _citaService = citaService;
        }
        public Task<int> Handle(AddRecetaRequest request, CancellationToken cancellationToken)
        {
            //  Validar paciente
            PersonaDto paciente = _personaService.GetById(request.Commnad.codigoPaciente, request.token);
            if (paciente == null)
            {
                throw new NotFoundException($"Paciente con codigo {request.Commnad.codigoPaciente} no existe.");
            }
            else
            {
                if (paciente.codigoTipoPersona == 1)
                {
                    throw new NotFoundException($"Persona con codigo {request.Commnad.codigoPaciente} no es paciente.");
                }
            }
            //Validar medico
            PersonaDto medico = _personaService.GetById(request.Commnad.codigoMedico, request.token);
            if (medico==null)
            {
                throw new NotFoundException($"Medico con codigo {request.Commnad.codigoPaciente} no existe.");
            }
            else
            {
                if (medico.codigoTipoPersona == 2)
                {
                    throw new NotFoundException($"Persona con codigo {request.Commnad.codigoPaciente} no es medico.");
                }
            }

            CitaDto cita = _citaService.GetById(request.Commnad.codigoCita, request.token);
            if (cita == null)
            {
                throw new NotFoundException($"La cita con codigo {request.Commnad.codigoCita} no existe.");
            }
            Receta receta = new Receta
            {
                codigoPaciente = request.Commnad.codigoPaciente,
                nombrePaciente = paciente.nombres+" "+ paciente.apellidos,
                codigoMedico = request.Commnad.codigoMedico,
                nombreMedico = medico.nombres + " " + medico.apellidos,
                fecha = request.Commnad.fecha,
                observacion = request.Commnad.observacion,
                codigoCita = request.Commnad.codigoCita,
                detalleReceta = request.Commnad.detalleReceta.Select((s, index) => new DetalleReceta
                {
                    numero = index + 1, // Calcula el número ascendente comenzando desde 1
                    nombreMedicamento = s.nombreMedicamento,
                    dosis = s.dosis,
                    frecuencia = s.frecuencia,

                }).ToList()
            };
            int result = _repository.Add(receta);
            return Task.FromResult(result);

        }
    }
}