using CitasMicroService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMicroService.Application.Queries
{
    public class GetCitaAllQuery:IRequest<List<Cita>>
    {
    }
}