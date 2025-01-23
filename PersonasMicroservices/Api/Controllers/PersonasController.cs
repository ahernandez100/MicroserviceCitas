using MediatR;
using PersonasMicroservices.Application.Commans;
using PersonasMicroservices.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace PersonasMicroservices.Api.Controllers
{
    [RoutePrefix("api/personas")]

    public class PersonasController : ApiController
    {
        private readonly IMediator _mediator;

        public PersonasController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddPersona([FromBody] AddPersonaCommand command)
        {
          int result=  _mediator.Send(command).Result;
            return Ok($"Persona agregada exitosamente con codigo {result}");
        }

        [HttpGet]
        [Route("{codigo:int}")]
        public IHttpActionResult GetPersonaById(int codigo)
        {
            var persona = _mediator.Send(new GetPersonaByIdQuery { codigo = codigo }).Result;
            return Ok(persona);
        }
    }
}
