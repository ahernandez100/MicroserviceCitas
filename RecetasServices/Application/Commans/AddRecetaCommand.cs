﻿using MediatR;
using RecetasServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecetasServices.Application.Commans
{
    public class AddRecetaCommand
    {
        public int codigoPaciente { get; set; }
        public int codigoMedico { get; set; }
        public DateTime fecha { get; set; }

        public string observacion { get; set; }
        public int codigoCita { get; set; }

        public virtual ICollection<AddDetalleRecetaCommand> detalleReceta { get; set; } = new List<AddDetalleRecetaCommand>();

    }
    public class AddDetalleRecetaCommand
    {
        public string nombreMedicamento { get; set; }
        public string dosis { get; set; }
        public string frecuencia { get; set; }
    }
}