using MediatR;
using PersonasMicroservices.Domain.Entities;
using PersonasMicroservices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;

namespace PersonasMicroservices.Application.Commans
{
    public class AddPersonaCommandHandler : IRequestHandler<AddPersonaCommand, int>
    {
        private readonly IPersonaRepository _repository;

        public AddPersonaCommandHandler(IPersonaRepository repository)
        {
            _repository = repository;
        }

        public Task<int> Handle(AddPersonaCommand request, CancellationToken cancellationToken)
        {
            Persona persona = new Persona
            {
                nombres = request.nombres,
                apellidos = request.apellidos,
                documento = request.documento,
                codigoTipoPersona = request.codigoTipoPersona,
                fechaNacimiento = request.fechaNacimiento,
                genero = request.genero,
                direccion = request.direccion,
                telefono = request.telefono,
                correoElectronico = request.correoElectronico,
            };
            int result = _repository.Add(persona);
            return Task.FromResult(result);
        }
    }
}