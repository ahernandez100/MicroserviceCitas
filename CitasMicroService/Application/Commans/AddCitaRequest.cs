using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMicroService.Application.Commans
{
    public class AddCitaRequest: IRequest<int>
    {
        public string token { get; set; }
        public AddCitaCommand Commnad { get; set; }
    }
}