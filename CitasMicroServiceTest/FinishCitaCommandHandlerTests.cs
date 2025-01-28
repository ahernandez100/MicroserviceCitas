using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CitasMicroService.Application.Commans;
using CitasMicroService.Domain.Entities;
using CitasMicroService.Domain.Repositories;
using CitasMicroService.Application.Servicio;
using System;
using System.Threading;
using System.Threading.Tasks;
using CitasMicroService.Api.Exeptions;
using System.Collections.Generic;

namespace CitasMicroServiceTest
{

        [TestClass]
        public class FinishCitaCommandHandlerTests
        {
            private Mock<ICitaRepository> _repositoryMock;
            private Mock<IRabbitMQSender> _rabbitMqSenderMock;
            private FinishCitaCommandHandler _handler;

            [TestInitialize]
            public void Setup()
            {
                _repositoryMock = new Mock<ICitaRepository>();
                _rabbitMqSenderMock = new Mock<IRabbitMQSender>();
                _handler = new FinishCitaCommandHandler(_repositoryMock.Object, _rabbitMqSenderMock.Object);
            }

            [TestMethod]
            [ExpectedException(typeof(KeyNotFoundException))]
            public async Task Handle_WhenCitaDoesNotExist_ThrowsKeyNotFoundException()
            {
                // Arrange
                var request = new FinishCitaRequest
                {
                    codigo = 1 // Cita con este código no existe
                };

                _repositoryMock.Setup(r => r.GetById(1)).Returns((Cita)null); // Cita no encontrada

                // Act
                await _handler.Handle(request, CancellationToken.None);

                // Assert: Excepción esperada
            }

            [TestMethod]
            public async Task Handle_WhenCitaExists_UpdatesEstadoAndSendsMessage()
            {
                // Arrange
                var cita = new Cita
                {
                    codigo = 1,
                    estado = "Pendiente",
                    fecha = DateTime.Now,
                    lugar = "Consultorio 1"
                };

                _repositoryMock.Setup(r => r.GetById(1)).Returns(cita); // Cita encontrada
                _repositoryMock.Setup(r => r.Update(It.IsAny<Cita>())).Verifiable(); // Verifica que se llame al método Update
                _rabbitMqSenderMock.Setup(r => r.SendMessage(It.IsAny<FinishCitaRequest>())).Verifiable(); // Verifica que se llame al método SendMessage

                var request = new FinishCitaRequest
                {
                    codigo = 1
                };

                // Act
                await _handler.Handle(request, CancellationToken.None);

                // Assert
                Assert.AreEqual("Finalizada", cita.estado); // Verifica que el estado de la cita ha sido actualizado a "Finalizada"
                _repositoryMock.Verify(r => r.Update(It.IsAny<Cita>()), Times.Once); // Verifica que se haya llamado a Update una vez
                _rabbitMqSenderMock.Verify(r => r.SendMessage(It.IsAny<FinishCitaRequest>()), Times.Once); // Verifica que se haya llamado a SendMessage una vez
            }
        }
    
}
