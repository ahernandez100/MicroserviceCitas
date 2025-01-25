using CitasMicroService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMicroService.Application.Queries
{
    public class GetCitaByIdQuery: IRequest<Cita>
    {
        public int codigo { get; set; }
    }
}