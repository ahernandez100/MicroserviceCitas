using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RecetasServices.Domain.Entities
{
    public class Receta
    {
        [Key]
        public int codigo { get; set; }
        public int codigoPaciente { get; set; }
        public string nombrePaciente { get; set; }

        public int codigoMedico  { get; set; }

        public string nombreMedico { get; set; }
        public DateTime fecha { get; set; }

        public string observacion { get; set; }
        public int codigoCita { get; set; }
        public virtual ICollection<DetalleReceta> detalleReceta { get; set; } = new List<DetalleReceta>();

    }

}