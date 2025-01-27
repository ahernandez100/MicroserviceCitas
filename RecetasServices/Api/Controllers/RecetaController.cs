using MediatR;
using RecetasServices.Api.Filters;
using RecetasServices.Application.Commans;
using RecetasServices.Application.Queries;
using RecetasServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RecetasServices.Api.Controllers
{
    [JwtAuthorizationFilter] // Aplica el filtro a todo el controlador
    [RoutePrefix("api/receta")]
    public class RecetaController : ApiController
    {

        private readonly IMediator _mediator;
        public RecetaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Obtiene una receta por su codigo
        /// </summary>
        /// <param name="codigo">Codigo de la receta</param>
        /// <returns>Receta si es encontrada, de lo contrario NotFound</returns>
        [HttpGet]
        [Route("{codigo:int}")]
        public IHttpActionResult GetRecetaById(int codigo)
        {
            Receta receta = _mediator.Send(new GetRecetaByIdQuery { codigo = codigo }).Result;
            return Ok(receta);
        }
        /// <summary>
        /// Crea una nueva receta
        /// </summary>
        /// <param name="command">Datos de la recra a crear</param>
        /// <returns>Mensaje  de éxito o error</returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult AddReceta([FromBody] AddRecetaCommand command)
        {
            int result = _mediator.Send(command).Result;
            return Ok($"Receta agregada exitosamente con codigo {result}");
        }
        /// <summary>
        /// Actualiza una receta existente
        /// </summary>
        /// <param name="codigo">Código de la receta actualizar</param>
        /// <param name="command">Datos de la receta a actualizar</param>
        /// <returns>Mensaje  de éxito o error</returns>
        [HttpPut]
        [Route("{codigo:int}")]
        public IHttpActionResult UpdtaePersona(int codigo, [FromBody] UpdateRecetaCommand command)
        {
            var request = new UpdateRecetaRequest
            {
                codigo = codigo,
                Command = command
            };
            _mediator.Send(request);
            return Ok($"Receta actualizada exitosamente");
        }
        /// <summary>
        /// Elimina una receta por su codigo
        /// </summary>
        /// <param name="codigo">Código de la receta a eliminar</param>
        /// <returns>Mensaje  de éxito o error</returns>
        [HttpDelete]
        [Route("{codigo:int}")]
        public IHttpActionResult Delete(int codigo)
        {

            int result = _mediator.Send(new DeleteRecetaCommand { codigo = codigo }).Result;
            return Ok($"Receta  con codigo {result} fue eliminada exitosamente");
        }

    }
}