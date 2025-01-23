using MediatR;
using PersonasMicroservices.Api.Exeptions;
using PersonasMicroservices.Domain.Entities;
using PersonasMicroservices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace PersonasMicroservices.Infrastructure
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly DatabaseContext _context;

        public PersonaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public int Add(Persona persona)
        {
            _context.Personas.Add(persona);
            _context.SaveChanges();
            return persona.codigo;
        }

        public Persona GetById(int id)
        {
            return _context.Personas.Find(id);
        }

        public void Update(Persona persona)
        {
            //Persona personaExiste = _context.Personas.Where(w => w.codigo == persona.codigo).FirstOrDefault();
            //personaExiste.nombres = persona.nombres;
            //personaExiste.apellidos = persona.apellidos;
            _context.Personas.AddOrUpdate(persona);
            _context.SaveChanges();
        }
    }
}