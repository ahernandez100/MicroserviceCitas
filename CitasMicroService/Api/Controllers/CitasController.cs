using CitasMicroService.Api.Filters;
using CitasMicroService.Application.Commans;
using CitasMicroService.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CitasMicroService.Api.Controllers
{
   // [JwtAuthorizationFilter] // Aplica el filtro a todo el controlador
    [RoutePrefix("api/citas")]
    public class CitasController : ApiController
    {
        private readonly IMediator _mediator;
        public CitasController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Obtiene una cita por su codigo
        /// </summary>
        /// <param name="codigo">Codigo de la cita</param>
        /// <returns>Cita si es encontrada, de lo contrario NotFound</returns>
        [HttpGet]
        [Route("{codigo:int}")]
        public IHttpActionResult GetCitaById(int codigo)
        {
            var cita = _mediator.Send(new GetCitaByIdQuery { codigo = codigo }).Result;
            return Ok(cita);
        }
        /// <summary>
        /// Obtiene todas las citas 
        /// </summary>
        /// <returns> lista de citas</returns>
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var cita = _mediator.Send(new GetCitaAllQuery()).Result;
            return Ok(cita);
        }
        /// <summary>
        /// Crea una nueva cita
        /// </summary>
        /// <param name="command">Datos de la cita a crear</param>
        /// <returns>Mensaje  de éxito o error</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddCita([FromBody] AddCitaCommand command)
        {
            var authHeader = Request.Headers.Authorization;
            var request = new AddCitaRequest
            {
                token = authHeader.Parameter,
                Commnad = command
            };
            int result = _mediator.Send(request).Result;
            return Ok($"Cita agregada exitosamente con codigo {result}");
        }
        /// <summary>
        /// Actualiza una cita existente
        /// </summary>
        /// <param name="codigo">Código de la cita  actualizar</param>
        /// <param name="command">Datos de la cita a actualizar</param>
        /// <returns>Mensaje  de éxito o error</returns>
        [HttpPut]
        [Route("{codigo:int}")]
        public IHttpActionResult UpdateCita(int codigo, [FromBody] UpdateCitaCommand command)
        {
            var request = new UpdateCitaRequest
            {
                codigo = codigo,
                Commnad = command
            };
            _mediator.Send(request);
            return Ok($"Cita actualizada exitosamente");
        }
        /// <summary>
        /// Elimina una cita por su codigo
        /// </summary>
        /// <param name="codigo">Código de la cita a eliminar</param>
        /// <returns>Mensaje  de éxito o error</returns>
        [HttpDelete]
        [Route("{codigo:int}")]
        public IHttpActionResult Delete(int codigo)
        {

            int result = _mediator.Send(new DeleteCitaCommand { codigo = codigo }).Result;
            return Ok($"Cita  con codigo {result} fue eliminada exitosamente");
        }
        [HttpPut]
        [Route("FinishCita/{codigo:int}")]
        public IHttpActionResult FinishCita(int codigo, [FromBody] FinishCitaCommand command)
        {
            var authHeader = Request.Headers.Authorization;
            var request = new FinishCitaRequest
            {
                codigo = codigo,
                token= authHeader.Parameter,
                Commnad = command
            };
            _mediator.Send(request);
            return Ok($"Cita Finalizada exitosamente");
        }
    }
}