using RecetasServices.Domain.Entities;
using RecetasServices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecetasServices.Infrastructure
{
    public class RecetaRepository:IRecetaRepository
    {
        private readonly DatabaseContext _context;

        public RecetaRepository(DatabaseContext context)
        {
            _context = context;
        }
        public Receta GetById(int codigo)
        {
            return _context.Recetas.Include("detalleReceta").FirstOrDefault(w => w.codigo == codigo);
 
        }
        public int Add(Receta receta)
        {
            var detalles = receta.detalleReceta;
            // Excluir los detalles al insertar la receta
            receta.detalleReceta = null;

            _context.Recetas.Add(receta);
            _context.SaveChanges();
            foreach (var itemDetalle in detalles)
            {
                itemDetalle.codigoReceta = receta.codigo;
            }
            // Agregar los detalles a la base de datos
            _context.DetalleRecetas.AddRange(detalles);
            _context.SaveChanges();
            return receta.codigo;
        }
        public void Update(Receta receta)
        {
            _context.Entry(receta).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public int Delete(int codigo)
        {
            Receta receta = _context.Recetas.Include("detalleReceta").FirstOrDefault(r => r.codigo == codigo);
            // Eliminar manualmente los detalles
            foreach (var detalle in receta.detalleReceta.ToList())
            {
                _context.Entry(detalle).State = EntityState.Deleted;
            }
            // Eliminar la receta
            _context.Recetas.Remove(receta);
            _context.SaveChanges();

            return codigo;
        }






    }
}