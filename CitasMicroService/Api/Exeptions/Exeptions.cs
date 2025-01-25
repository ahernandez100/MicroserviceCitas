using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasMicroService.Api.Exeptions
{
    public class NotFoundException : Exception
    {
        // Constructor sin parámetros
        public NotFoundException() : base("El recurso solicitado no fue encontrado.") { }

        // Constructor con un mensaje personalizado
        public NotFoundException(string message) : base(message) { }

        // Constructor con mensaje y la excepción interna
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}