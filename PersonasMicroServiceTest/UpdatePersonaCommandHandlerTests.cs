using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PersonasMicroservices.Application.Commans;
using PersonasMicroservices.Domain.Entities;
using PersonasMicroservices.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PersonasMicroServiceTest
{
    [TestClass]
    public class UpdatePersonaCommandHandlerTests
    {

        private Mock<IPersonaRepository> _repositoryMock;
        private UpdatePersonaCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IPersonaRepository>();
            _handler = new UpdatePersonaCommandHandler(_repositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_WhenPersonaExists_UpdatesPersona()
        {
            // Arrange
            var existingPersona = new Persona
            {
                codigo = 1,
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

            var updateCommand = new UpdatePersonaRequest
            {
                codigo = 1,
                Command = new UpdatePersonaCommand
                {
                    nombres = "Carlos Alberto",
                    apellidos = "Pérez Gómez",
                    documento = "87654321",
                    codigoTipoPersona = 2,
                    fechaNacimiento = new DateTime(1985, 10, 20),
                    genero = "Masculino",
                    direccion = "Avenida Nueva 456",
                    telefono = "555-5678",
                    correoElectronico = "carlos.alberto@example.com"
                }
            };

            _repositoryMock.Setup(r => r.GetById(1)).Returns(existingPersona);

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Update(It.Is<Persona>(p =>
                p.codigo == 1 &&
                p.nombres == updateCommand.Command.nombres &&
                p.apellidos == updateCommand.Command.apellidos &&
                p.documento == updateCommand.Command.documento &&
                p.codigoTipoPersona == updateCommand.Command.codigoTipoPersona &&
                p.fechaNacimiento == updateCommand.Command.fechaNacimiento &&
                p.genero == updateCommand.Command.genero &&
                p.direccion == updateCommand.Command.direccion &&
                p.telefono == updateCommand.Command.telefono &&
                p.correoElectronico == updateCommand.Command.correoElectronico
            )), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task Handle_WhenPersonaDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var updateCommand = new UpdatePersonaRequest
            {
                codigo = 99,
                Command = new UpdatePersonaCommand
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
                }
            };

            _repositoryMock.Setup(r => r.GetById(99)).Returns((Persona)null);

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);

            // Assert: No es necesario, lo maneja ExpectedException
        }

        [TestMethod]
        public async Task Handle_VerifyRepositoryUpdateCalledWithCorrectData()
        {
            // Arrange
            var existingPersona = new Persona
            {
                codigo = 10,
                nombres = "Ana",
                apellidos = "García",
                documento = "12345678",
                codigoTipoPersona = 1,
                fechaNacimiento = new DateTime(1990, 5, 15),
                genero = "Femenino",
                direccion = "Calle Ejemplo 123",
                telefono = "555-1234",
                correoElectronico = "ana.garcia@example.com"
            };

            var updateCommand = new UpdatePersonaRequest
            {
                codigo = 10,
                Command = new UpdatePersonaCommand
                {
                    nombres = "Ana María",
                    apellidos = "García López",
                    documento = "87654321",
                    codigoTipoPersona = 2,
                    fechaNacimiento = new DateTime(1985, 10, 20),
                    genero = "Femenino",
                    direccion = "Avenida Nueva 456",
                    telefono = "555-5678",
                    correoElectronico = "ana.maria@example.com"
                }
            };

            _repositoryMock.Setup(r => r.GetById(10)).Returns(existingPersona);

            // Act
            await _handler.Handle(updateCommand, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Update(It.Is<Persona>(p =>
                p.codigo == 10 &&
                p.nombres == updateCommand.Command.nombres &&
                p.apellidos == updateCommand.Command.apellidos &&
                p.documento == updateCommand.Command.documento &&
                p.codigoTipoPersona == updateCommand.Command.codigoTipoPersona &&
                p.fechaNacimiento == updateCommand.Command.fechaNacimiento &&
                p.genero == updateCommand.Command.genero &&
                p.direccion == updateCommand.Command.direccion &&
                p.telefono == updateCommand.Command.telefono &&
                p.correoElectronico == updateCommand.Command.correoElectronico
            )), Times.Once);

        }
    }
}
