﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CitasMicroService.Application.Commans
{
    public class UpdateCitaCommand
    {
        public string estado { get; set; }
    }
}