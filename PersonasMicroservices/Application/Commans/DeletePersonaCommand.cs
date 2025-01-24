using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonasMicroservices.Application.Commans
{
    public class DeletePersonaCommand : IRequest<int>
    {
        public int codigo { get; set; }
    }
}