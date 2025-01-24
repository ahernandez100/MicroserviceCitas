using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonasMicroservices.Application.Commans
{
    public class UpdatePersonaRequest : IRequest<Unit>
    {
        public int codigo { get; set; }
        public UpdatePersonaCommand Command { get; set; }
    }
}