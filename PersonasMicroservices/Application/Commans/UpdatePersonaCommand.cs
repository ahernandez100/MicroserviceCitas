using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonasMicroservices.Application.Commans
{
    public class UpdatePersonaCommand
    {
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string documento { get; set; }
        public int codigoTipoPersona { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string genero { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correoElectronico { get; set; }
    }
}