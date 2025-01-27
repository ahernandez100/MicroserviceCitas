using RecetasServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RecetasServices.Infrastructure
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=RecetasContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<DetalleReceta> DetalleRecetas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Receta>().
                ToTable("Recetas");
            modelBuilder.Entity<DetalleReceta>()
                .ToTable("DetalleRecetas");
            base.OnModelCreating(modelBuilder);
        }


    }
}