using RecetasServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RecetasServices.Application.Commans
{
    public class UpdateRecetaCommand
    {
        public string estado { get; set; }

    }

}