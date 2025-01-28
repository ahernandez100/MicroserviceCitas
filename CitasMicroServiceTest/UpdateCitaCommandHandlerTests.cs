using CitasMicroService.Application.Commans;
using CitasMicroService.Domain.Entities;
using CitasMicroService.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace CitasMicroServiceTest
{
    [TestClass]
    public class UpdateCitaCommandHandlerTests
    {
        private Mock<ICitaRepository> _repositoryMock;
        private UpdateCitaCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<ICitaRepository>();
            _handler = new UpdateCitaCommandHandler(_repositoryMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task Handle_WhenCitaDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var request = new UpdateCitaRequest
            {
                codigo = 1, // Cita con este código no existe
                Commnad = new UpdateCitaCommand
                {
                    estado = "Confirmada"
                }
            };

            _repositoryMock.Setup(r => r.GetById(1)).Returns((Cita)null); // Cita no encontrada

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: Excepción esperada
        }

        [TestMethod]
        public async Task Handle_WhenCitaExists_UpdatesCitaState()
        {
            // Arrange
            var request = new UpdateCitaRequest
            {
                codigo = 1, // Cita con este código existe
                Commnad = new UpdateCitaCommand
                {
                    estado = "Confirmada"
                }
            };

            var cita = new Cita
            {
                codigo = 1,
                estado = "Pendiente"
            };

            _repositoryMock.Setup(r => r.GetById(1)).Returns(cita); // Cita encontrada
            _repositoryMock.Setup(r => r.Update(It.IsAny<Cita>())).Verifiable(); // Verifica que el método Update se llama

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.AreEqual("Confirmada", cita.estado); // Verifica que el estado de la cita fue actualizado
            _repositoryMock.Verify(r => r.Update(It.IsAny<Cita>()), Times.Once); // Verifica que se llamó una vez al método Update
        }
    }
}
