﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMicroService.Application.Commans
{
    public class AddCitaCommand 
    {
        public int codigoPaciente { get; set; }
        public int codigoMedico { get; set; }
        public DateTime fecha { get; set; }
        public string lugar { get; set; }
        public string estado { get; set; }
    }
}