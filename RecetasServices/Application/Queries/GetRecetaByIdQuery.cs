using MediatR;
using RecetasServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecetasServices.Application.Queries
{
    public class GetRecetaByIdQuery:IRequest<Receta>
    {
        public int codigo { get; set; }
    }
}