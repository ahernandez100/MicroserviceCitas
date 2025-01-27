using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecetasServices.Application.Commans
{
    public class DeleteRecetaCommand : IRequest<int>
    {
        public int codigo { get; set; }
    }
}