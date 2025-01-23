using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Newtonsoft.Json;


namespace PersonasMicroservices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API

            // Configurar la serialización de JSON
            var jsonSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented; // Para respuestas más legibles
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); // Usar camelCase para las propiedades

            // Eliminar el soporte para XML (opcional)
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
