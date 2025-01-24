using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CitasMicroService.Domain.Entities
{
    public class Cita
    {
        [Key]
        public int codigo { get; set; }
        public int codigoPaciente { get; set; }
        [StringLength(400)]
        public string nombrePaciente { get; set; }
        public int codigoMedico { get; set; }
        [StringLength(400)]
        public string nombreMedico { get; set; }
        public DateTime fecha { get; set; }
        [StringLength(200)]
        public string lugar { get; set; }
        [RegularExpression("^(Pendiente|En Proceso|Finalizada)$", ErrorMessage = "El estado debe ser 'Pendiente', 'En Proceso' o 'Finalizada'.")]
        [StringLength(50)]
        public string estado { get; set; }

    }
}