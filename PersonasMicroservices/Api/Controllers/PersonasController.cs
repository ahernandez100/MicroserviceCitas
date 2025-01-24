using MediatR;
using PersonasMicroservices.Application.Commans;
using PersonasMicroservices.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        /// <summary>
        /// Obtiene una persona por su codigo
        /// </summary>
        /// <param name="codigo">Codigo de la persona</param>
        /// <returns>Persona si es encontrada, de lo contrario NotFound</returns>
        [HttpGet]
        [Route("{codigo:int}")]
        public IHttpActionResult GetPersonaById(int codigo)
        {
            var persona = _mediator.Send(new GetPersonaByIdQuery { codigo = codigo }).Result;
            return Ok(persona);
        }
        /// <summary>
        /// Obtiene todas las peronas 
        /// </summary>
        /// <returns> lista de personas</returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var persona = _mediator.Send(new GetPersonaAllQuery()).Result;
            return Ok(persona);
        }
        /// <summary>
        /// Crea una nueva persona
        /// </summary>
        /// <param name="command">Datos de la persona a crear</param>
        /// <returns>Mensaje  de éxito o error</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddPersona([FromBody] AddPersonaCommand command)
        {
            int result = _mediator.Send(command).Result;
            return Ok($"Persona agregada exitosamente con codigo {result}");
        }
        /// <summary>
        /// Actualiza una persona existente
        /// </summary>
        /// <param name="codigo">Código de la persona actualizar</param>
        /// <param name="command">Datos de la persona a actualizar</param>
        /// <returns>Mensaje  de éxito o error</returns>
        [HttpPut]
        [Route("{codigo:int}")]
        public IHttpActionResult UpdtaePersona(int codigo, [FromBody] UpdatePersonaCommand command)
        {
            var request = new UpdatePersonaRequest
            {
                codigo = codigo,
                Command = command
            };
            _mediator.Send(request);
            return Ok($"Persona actualizada exitosamente");
        }
        /// <summary>
        /// Elimina una persona por su codigo
        /// </summary>
        /// <param name="codigo">Código de la persona a eliminar</param>
        /// <returns>Mensaje  de éxito o error</returns>
        [HttpDelete]
        [Route("{codigo:int}")]
        public IHttpActionResult Delete(int codigo)
        {

            int result = _mediator.Send(new DeletePersonaCommand { codigo = codigo }).Result;
            return Ok($"Persona  con codigo {result} fue eliminada exitosamente");
        }

    }
}
