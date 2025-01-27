using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecetasServices.Application.Commans
{
    public class UpdateRecetaRequest : IRequest<Unit>
    {
        public int codigo { get; set; }
        public UpdateRecetaCommand Command { get; set; }

    }
}