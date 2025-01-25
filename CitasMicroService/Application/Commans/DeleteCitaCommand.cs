using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMicroService.Application.Commans
{
    public class DeleteCitaCommand : IRequest<int>
    {
        public int codigo { get; set; }
    }
}