﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecetasServices.Application.DTO
{
    public class PersonaDto
    {
        public int codigo { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string documento { get; set; }
        public int codigoTipoPersona { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string genero { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correoElectronico { get; set; }
        public bool estado { get; set; }
        public TipoPersonaDto tipoPersona { get; set; } = null;
    }
    public class TipoPersonaDto
    {

        public int codigo { get; set; }
        public string descripcion { get; set; }
    }

}