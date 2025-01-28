using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestSharp;
using RecetasServices.Application.Services;
using RecetasServices.Application.DTO;
using System;
using System.Threading.Tasks;
namespace RecetasMicroServiceTest
{

    [TestClass]
    public class PersonaServiceTests
    {
        private Mock<RestClient> _clientMock;
        private PersonaService _personaService;
        private  string _uriPersona;
        [TestInitialize]
        public void Setup()
        {
            _clientMock = new Mock<RestClient>("http://mockurl.com");
            _personaService = new PersonaService(); // El constructor debería inicializar el RestClient correctamente.
            _uriPersona = "api/personas";
        }



    }
}

