using RecetasServices.Application.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RecetasServices.Application.Services
{
    public class CitaService : ICitaService
    {
        private readonly RestClient _client;
        private readonly string _uri;
        public CitaService()
        {
            _client = new RestClient(ConfigurationManager.AppSettings["citasApiUrl"]);
            _uri = "api/citas";

        }
        public  CitaDto GetById(int id, string token)
        {
            var request = new RestRequest($"{_uri}/{id}", Method.Get);
            request.AddHeader("Authorization", "Bearer " + token);
            var response =  _client.Execute<CitaDto>(request);

            if (response.IsSuccessful && response.Data != null)
            {
                return response.Data;
            }
            else
            {
                return null; // O puedes manejar errores dependiendo de la respuesta
            }
        }

    }
}