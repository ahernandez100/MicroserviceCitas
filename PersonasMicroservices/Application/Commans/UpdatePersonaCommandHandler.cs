using MediatR;
using PersonasMicroservices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using PersonasMicroservices.Domain.Entities;
using System.Web.Http.Results;

namespace PersonasMicroservices.Application.Commans
{
    public class UpdatePersonaCommandHandler : IRequestHandler<UpdatePersonaRequest, Unit>
    {
        private readonly IPersonaRepository _repository;
        public UpdatePersonaCommandHandler(IPersonaRepository repository)
        {
            _repository = repository;
        }
        public Task<Unit> Handle(UpdatePersonaRequest request, CancellationToken cancellationToken)
        {
            // Buscar la persona en la base de datos
            Persona persona = _repository.GetById(request.codigo);

            if (persona == null)
            {
                throw new KeyNotFoundException($"No se encontró una persona con el codigo {request.codigo}");
            }
            persona.nombres = request.Command.nombres;
            persona.apellidos = request.Command.apellidos;
            persona.documento = request.Command.documento;
            persona.codigoTipoPersona = request.Command.codigoTipoPersona;
            persona.fechaNacimiento = request.Command.fechaNacimiento;
            persona.genero = request.Command.genero;
            persona.direccion = request.Command.direccion;
            persona.telefono = request.Command.telefono;
            persona.correoElectronico = request.Command.correoElectronico;
            _repository.Update(persona);
            return Task.FromResult(Unit.Value); // Retorna un valor vacío (indica éxito)



        }
    }
}