using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonasMicroservices.Application.Commans;
using PersonasMicroservices.Domain.Entities;
using PersonasMicroservices.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PersonasMicroServiceTest
{
    [TestClass]
    public class AddPersonaCommandHandlerTests
    {
        private Mock<IPersonaRepository> _repositoryMock;
        private AddPersonaCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IPersonaRepository>();
            _handler = new AddPersonaCommandHandler(_repositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_WhenCalled_ReturnsPersonaId()
        {
            // Arrange
            var command = new AddPersonaCommand
            {
                nombres = "Carlos",
                apellidos = "Pérez",
                documento = "12345678",
                codigoTipoPersona = 1,
                fechaNacimiento = new DateTime(1990, 5, 15),
                genero = "Masculino",
                direccion = "Calle Ejemplo 123",
                telefono = "555-1234",
                correoElectronico = "carlos.perez@example.com"
            };

            _repositoryMock
                .Setup(r => r.Add(It.IsAny<Persona>()))
                .Returns(1); // Simulamos que devuelve el ID 1

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task Handle_VerifyRepositoryCalledWithCorrectData()
        {
            // Arrange
            var command = new AddPersonaCommand
            {
                nombres = "Ana",
                apellidos = "García",
                documento = "87654321",
                codigoTipoPersona = 2,
                fechaNacimiento = new DateTime(1985, 10, 20),
                genero = "Femenino",
                direccion = "Avenida Principal 456",
                telefono = "555-6789",
                correoElectronico = "ana.garcia@example.com"
            };

            _repositoryMock
                .Setup(r => r.Add(It.IsAny<Persona>()))
                .Returns(2);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Add(It.Is<Persona>(p =>
                p.nombres == command.nombres &&
                p.apellidos == command.apellidos &&
                p.documento == command.documento &&
                p.codigoTipoPersona == command.codigoTipoPersona &&
                p.fechaNacimiento == command.fechaNacimiento &&
                p.genero == command.genero &&
                p.direccion == command.direccion &&
                p.telefono == command.telefono &&
                p.correoElectronico == command.correoElectronico
            )), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task Handle_WhenRepositoryThrowsException_PropagatesException()
        {
            // Arrange
            var command = new AddPersonaCommand
            {
                nombres = "Luis",
                apellidos = "Martínez",
                documento = "11223344",
                codigoTipoPersona = 3,
                fechaNacimiento = new DateTime(2000, 8, 30),
                genero = "Masculino",
                direccion = "Calle Secundaria 789",
                telefono = "555-9876",
                correoElectronico = "luis.martinez@example.com"
            };

            _repositoryMock
                .Setup(r => r.Add(It.IsAny<Persona>()))
                .Throws(new Exception("Error inesperado en el repositorio"));

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert: No es necesario, lo maneja ExpectedException
        }
    }
}
