using CitasMicroService.Application.Commans;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CitasMicroService.Api.Controllers
{
    [RoutePrefix("api/citas")]
    public class CitasController : ApiController
    {
        private readonly IMediator _mediator;
        public CitasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult AddCita([FromBody] AddCitaCommand command)
        {
            int result = _mediator.Send(command).Result;
            return Ok($"Cita agregada exitosamente con codigo {result}");
        }
    }
}