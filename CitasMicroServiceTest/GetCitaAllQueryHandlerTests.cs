using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CitasMicroService.Application.Queries;
using CitasMicroService.Domain.Entities;
using CitasMicroService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CitasMicroServiceTest
{
    [TestClass]
    public class GetCitaAllQueryHandlerTests
    {
        private Mock<ICitaRepository> _repositoryMock;
        private GetCitaAllQueryHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<ICitaRepository>();
            _handler = new GetCitaAllQueryHandler(_repositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_WhenCitasExist_ReturnsCitaList()
        {
            // Arrange
            var citas = new List<Cita>
            {
                new Cita { codigo = 1, codigoPaciente = 101, codigoMedico = 201, fecha = DateTime.Now, lugar = "Hospital Central", estado = "Pendiente" },
                new Cita { codigo = 2, codigoPaciente = 102, codigoMedico = 202, fecha = DateTime.Now.AddDays(1), lugar = "Clínica A", estado = "Confirmada" }
            };

            _repositoryMock.Setup(r => r.GetAll()).Returns(citas);

            // Act
            var result = await _handler.Handle(new GetCitaAllQuery(), CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].codigo);
            Assert.AreEqual(2, result[1].codigo);
        }

        [TestMethod]
        public async Task Handle_WhenNoCitasExist_ReturnsEmptyList()
        {
            // Arrange
            var citas = new List<Cita>(); // Lista vacía
            _repositoryMock.Setup(r => r.GetAll()).Returns(citas);

            // Act
            var result = await _handler.Handle(new GetCitaAllQuery(), CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count); // Asegúrate de que sea una lista vacía
        }

        [TestMethod]
        public async Task Handle_VerifyRepositoryGetAllCalledOnce()
        {
            // Arrange
            var citas = new List<Cita>
            {
                new Cita { codigo = 1, codigoPaciente = 101, codigoMedico = 201, fecha = DateTime.Now, lugar = "Hospital Central", estado = "Pendiente" }
            };

            _repositoryMock.Setup(r => r.GetAll()).Returns(citas);

            // Act
            await _handler.Handle(new GetCitaAllQuery(), CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.GetAll(), Times.Once); // Verifica que el método GetAll se llamó exactamente una vez
        }
    }
}
