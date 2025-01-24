using CitasMicroService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CitasMicroService.Infrastructure
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext() : base("name=CitasContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Cita> Citas { get; set; }
 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>().
                ToTable("Citas");
        }
    }
}