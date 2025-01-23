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
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string documento { get; set; }
        public int codigoTipoPersona { get; set; }
        public DateTime fechaNacimiento { get; set; }
        [StringLength(1)]
        public string genero { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correoElectronico { get; set; }

        [ForeignKey("codigoTipoPersona")]
        public virtual TipoPersona tipoPersona { get; set; }

    }
    /*
     * 
     */
}