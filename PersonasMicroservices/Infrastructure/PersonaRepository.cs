using MediatR;
using PersonasMicroservices.Api.Exeptions;
using PersonasMicroservices.Domain.Entities;
using PersonasMicroservices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
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

        public List<Persona> GetAll()
        { 
            return _context.Personas.Include("TipoPersona").Where(w => w.estado == true).ToList();
        }

        public void Update(Persona persona)
        {
            _context.Entry(persona).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public int Delete(int codigo)
        {

            Persona persona = _context.Personas.Find(codigo);
            persona.estado = false;
            _context.SaveChanges();
             return codigo;
        }


    }
}