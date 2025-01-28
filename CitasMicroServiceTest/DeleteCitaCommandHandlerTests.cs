using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CitasMicroService.Application.Commans;
using CitasMicroService.Domain.Entities;
using CitasMicroService.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;
using CitasMicroService.Api.Exeptions;

namespace CitasMicroServiceTest
{
    [TestClass]
    public class DeleteCitaCommandHandlerTests
    {
        private Mock<ICitaRepository> _repositoryMock;
        private DeleteCitaCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<ICitaRepository>();
            _handler = new DeleteCitaCommandHandler(_repositoryMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenCitaDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var request = new DeleteCitaCommand
            {
                codigo = 1 // Cita con este código no existe
            };

            _repositoryMock.Setup(r => r.GetById(1)).Returns((Cita)null); // Cita no encontrada

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: Excepción esperada
        }

        [TestMethod]
        public async Task Handle_WhenCitaExists_DeletesCita()
        {
            // Arrange
            var request = new DeleteCitaCommand
            {
                codigo = 1 // Cita con este código existe
            };

            var cita = new Cita
            {
                codigo = 1,
                estado = "Pendiente"
            };

            _repositoryMock.Setup(r => r.GetById(1)).Returns(cita); // Cita encontrada
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(1); // Simula la eliminación

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.AreEqual(1, result); // Verifica que el valor retornado sea el esperado (indicando que se eliminó correctamente)
            _repositoryMock.Verify(r => r.Delete(It.IsAny<int>()), Times.Once); // Verifica que se llamó una vez al método Delete
        }
    }
}
