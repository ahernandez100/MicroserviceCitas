using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CitasMicroService.Application.Queries;
using CitasMicroService.Domain.Entities;
using CitasMicroService.Domain.Repositories;
using CitasMicroService.Api.Exeptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CitasMicroServiceTest
{
    [TestClass]
    public class GetCitaByIdQueryHandlerTests
    {
        private Mock<ICitaRepository> _repositoryMock;
        private GetCitaByIdQueryHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<ICitaRepository>();
            _handler = new GetCitaByIdQueryHandler(_repositoryMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenCitaDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var request = new GetCitaByIdQuery
            {
                codigo = 1 // Cita con este código no existe
            };

            _repositoryMock.Setup(r => r.GetById(1)).Returns((Cita)null); // Cita no encontrada

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: Excepción esperada
        }

        [TestMethod]
        public async Task Handle_WhenCitaExists_ReturnsCita()
        {
            // Arrange
            var cita = new Cita
            {
                codigo = 1,
                estado = "Confirmada",
                fecha = DateTime.Now,
                lugar = "Consultorio 1"
            };

            _repositoryMock.Setup(r => r.GetById(1)).Returns(cita); // Cita encontrada

            var request = new GetCitaByIdQuery
            {
                codigo = 1
            };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result); // Verifica que el resultado no es nulo
            Assert.AreEqual(1, result.codigo); // Verifica que la cita devuelta tiene el código 1
            Assert.AreEqual("Confirmada", result.estado); // Verifica que el estado de la cita es "Confirmada"
        }
    }

}
