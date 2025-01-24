using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PersonasMicroservices.Domain.Entities
{
    public class Persona
    {
        [Key]
        public int codigo { get; set; }
        [StringLength(200)]
        public string nombres { get; set; }
        [StringLength(200)]
        public string apellidos { get; set; }
        [StringLength(200)]
        public string documento { get; set; }
        public int codigoTipoPersona { get; set; }
        public DateTime fechaNacimiento { get; set; }
        [RegularExpression("^[FM]$", ErrorMessage = "El género debe ser 'F' o 'M'.")]
        [StringLength(1)]
        public string genero { get; set; }
        [StringLength(255)]
        public string direccion { get; set; }
        [StringLength(15)]
        public string telefono { get; set; }
        [StringLength(100)]
        public string correoElectronico { get; set; }
        public bool estado { get; set; } = true;

        [ForeignKey("codigoTipoPersona")]
        public virtual TipoPersona tipoPersona { get; set; }

    }
}