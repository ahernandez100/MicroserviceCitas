using PersonasMicroservices.Domain.Entities;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonasMicroservices.Infrastructure
{

    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=PersonasContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<TipoPersona> TipoPersonas { get; set; }
  
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>().
                ToTable("Personas");
            modelBuilder.Entity<TipoPersona>()
                .ToTable("TipoPersonas");
            base.OnModelCreating(modelBuilder);
        }


    }

}