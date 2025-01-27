using RecetasServices.Api.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace RecetasServices.Api.Filters
{
    public class ExeptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;
            object errorResponse;

            // Usar un switch para manejar diferentes tipos de excepciones
            switch (exception)
            {
                case ArgumentNullException _:
                    // Respuesta personalizada para ArgumentNullException
                    errorResponse = new
                    {
                        message = "Un parámetro requerido está vacío.",
                        statusCode = HttpStatusCode.BadRequest
                    };
                    context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
                    break;

                case UnauthorizedAccessException _:
                    // Respuesta personalizada para UnauthorizedAccessException
                    errorResponse = new
                    {
                        message = "Acceso no autorizado.",
                        statusCode = HttpStatusCode.Unauthorized
                    };
                    context.Response = context.Request.CreateResponse(HttpStatusCode.Unauthorized, errorResponse);
                    break;

                case InvalidOperationException _:
                    // Respuesta personalizada para InvalidOperationException
                    errorResponse = new
                    {
                        message = "Operación no válida.",
                        statusCode = HttpStatusCode.BadRequest
                    };
                    context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, errorResponse);
                    break;
                case NotFoundException notFoundException:
                    // Respuesta personalizada para NotFoundException
                    errorResponse = new
                    {
                        message = notFoundException.Message,
                        statusCode = HttpStatusCode.NotFound
                    };
                    context.Response = context.Request.CreateResponse(HttpStatusCode.NotFound, errorResponse);
                    break;
                case Exception _:
                    // Respuesta general para cualquier otra excepción
                    errorResponse = new
                    {
                        message = exception.Message,
                        detail = exception.StackTrace,
                        statusCode = HttpStatusCode.InternalServerError
                    };
                    context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, errorResponse);
                    break;
            }

            // Aquí puedes agregar un log si lo deseas
            // LogException(exception);
        }
    }
}