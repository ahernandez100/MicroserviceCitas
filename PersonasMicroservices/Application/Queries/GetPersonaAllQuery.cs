using MediatR;
using PersonasMicroservices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonasMicroservices.Application.Queries
{
    public class GetPersonaAllQuery : IRequest<List<Persona>>
    {

    }
}