using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonasMicroservices.Domain.Entities
{
    public class TipoPersona
    {
        [Key]
        public int codigo { get; set; }
        [StringLength(100)]
        public string descripcion { get; set; }
    }
}