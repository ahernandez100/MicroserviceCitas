using CitasMicroService.Application.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CitasMicroService.Application.Servicio
{
    public class PersonaService : IPersonaService
    {
        private readonly RestClient _clientPersona;
        private readonly string _uriPersona;
        public PersonaService()
        {
            _clientPersona = new RestClient(ConfigurationManager.AppSettings["personasApiUrl"]);
            _uriPersona = "api/personas";
        }
        public PersonaDto GetById(int codigo, string token)
        {
            // Validación de parámetro
            if (codigo <= 0)
            {
                throw new System.ArgumentException("El codigo de la persona no puede ser cero o menor a cero");
            }
            // Construcción de la URL con los parámetros api personas
            var request = new RestRequest($"{_uriPersona}/{codigo}", Method.Get);
            request.AddHeader("Authorization", "Bearer " + token);

            // Realiza la llamada al servicio web de personas
            var response = _clientPersona.Execute<PersonaDto>(request);

            // Verifica si la respuesta es exitosa
            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            // Manejo de errores o respuestas no exitosas
            return null; // Aquí retornamos null para manejarlo en el controlador
        }

    }
}