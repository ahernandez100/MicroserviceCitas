using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecetasServices.Application.Commans
{
    public class AddRecetaRequest: IRequest<int>
    {
        public int codigo { get; set; }
        public string token { get; set; }
        public AddRecetaCommand Commnad { get; set; }
    }
}