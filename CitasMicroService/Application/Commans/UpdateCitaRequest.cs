using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMicroService.Application.Commans
{
    public class UpdateCitaRequest: IRequest<Unit>
    {
        public int codigo { get; set; }
        public UpdateCitaCommand Commnad { get; set; }

    }
}