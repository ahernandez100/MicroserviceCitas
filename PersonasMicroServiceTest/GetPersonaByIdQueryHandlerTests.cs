using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using System.Threading;
using PersonasMicroservices.Domain.Repositories;
using PersonasMicroservices.Application.Queries;
using PersonasMicroservices.Domain.Entities;
using PersonasMicroservices.Api.Exeptions;

namespace PersonasMicroServiceTest
{
    [TestClass]
    public class GetPersonaByIdQueryHandlerTests
    {
        private Mock<IPersonaRepository> _repositoryMock;
        private GetPersonaByIdQueryHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IPersonaRepository>();
            _handler = new GetPersonaByIdQueryHandler(_repositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_WhenPersonaExists_ReturnsPersona()
        {
            // Arrange
            var persona = new Persona
            {
                codigo = 1,
                nombres = "Juan",
                apellidos = "Pérez",
                documento = "12345678",
                codigoTipoPersona=1,
                fechaNacimiento=DateTime.Now,
                genero="M",
                direccion="molee",
                telefono="315666",
                correoElectronico="none@gmail.com",
                estado=true
            };
            var query = new GetPersonaByIdQuery { codigo = 1 };

            _repositoryMock
                .Setup(r => r.GetById(1))
                .Returns(persona);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(persona.codigo, result.codigo);
            Assert.AreEqual(persona.nombres, result.nombres);
            Assert.AreEqual(persona.apellidos, result.apellidos);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenPersonaDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var query = new GetPersonaByIdQuery { codigo = 99 };

            _repositoryMock
                .Setup(r => r.GetById(99))
                .Returns((Persona)null);

            // Act
            await _handler.Handle(query, CancellationToken.None);

            // Assert (handled by ExpectedException)
        }

    }
}
