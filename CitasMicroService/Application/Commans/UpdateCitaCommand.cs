using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CitasMicroService.Application.Commans
{
    public class UpdateCitaCommand
    {
        public int codigoPaciente { get; set; }
        public string nombrePaciente { get; set; }
        public int codigoMedico { get; set; }
        public string nombreMedico { get; set; }
        public DateTime fecha { get; set; }
        public string lugar { get; set; }
        public string estado { get; set; }
    }
}