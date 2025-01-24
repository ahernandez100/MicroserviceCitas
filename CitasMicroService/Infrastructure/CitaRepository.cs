﻿using CitasMicroService.Domain.Entities;
using CitasMicroService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMicroService.Infrastructure
{
    public class CitaRepository : ICitaRepository
    {
        private readonly DatabaseContext _context;
        public CitaRepository(DatabaseContext context)
        {
            _context = context;
        }
        public int Add(Cita cita)
        {
            _context.Citas.Add(cita);
            _context.SaveChanges();
            return cita.codigo;
        }
        public Cita GetById(int codigo)
        {
            return _context.Citas.Find(codigo);
        }
    }
}