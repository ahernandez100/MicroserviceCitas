using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMicroService.Application.Commans
{
    public class FinishCitaRequest:IRequest<Unit>
    {
        public int codigo { get; set; }
        public string token { get; set; }
        public FinishCitaCommand Commnad { get; set; }
    }
}