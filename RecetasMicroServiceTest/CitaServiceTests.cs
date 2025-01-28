using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecetasServices.Application.DTO;
using RecetasServices.Application.Services;
using RestSharp;
using System;
using System.Net;

namespace RecetasMicroServiceTest
{
    [TestClass]
    public class CitaServiceTests
    {
        private Mock<RestClient> _mockRestClient;
        private CitaService _citaService;
        private string _validToken = "valid-token";
        private  string _uri;
        private  RestClient _client;
        [TestInitialize]
        public void Setup()
        {
            // Mock RestClient
            _mockRestClient = new Mock<RestClient>("http://localhost");
            _citaService = new CitaService(); // Inyección de RestClient simulado
            _uri = "api/recetas";
            _client = new RestClient("https://localhost:44346");
        }


    }
}
