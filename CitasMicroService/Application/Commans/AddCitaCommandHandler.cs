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
using CitasMicroService.Api.Exeptions;
using CitasMicroService.Application.DTO;
using CitasMicroService.Application.Servicio;

namespace CitasMicroService.Application.Commans
{
    public class AddCitaCommandHandler : IRequestHandler<AddCitaRequest, int>
    {
        private readonly ICitaRepository _repository;
        private readonly IPersonaService _personaService;
        public AddCitaCommandHandler(ICitaRepository repository, IPersonaService personaService)
        {
            _repository = repository;
            _personaService = personaService;
        }
        public Task<int> Handle(AddCitaRequest request, CancellationToken cancellationToken)
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
            if (medico == null)
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
            Cita cita = new Cita
            {
                codigoPaciente = request.Commnad.codigoPaciente,
                nombrePaciente = paciente.nombres +" "+ paciente.apellidos,
                codigoMedico = request.Commnad.codigoMedico,
                nombreMedico = medico.nombres + " " + medico.apellidos,
                fecha = request.Commnad.fecha,
                lugar = request.Commnad.lugar,
                estado = request.Commnad.estado,
            };
            int result = _repository.Add(cita);
            return Task.FromResult(result);
        }
    }
}