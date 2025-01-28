using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecetasServices.Application.Commans;
using RecetasServices.Domain.Entities;
using RecetasServices.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;
using RecetasServices.Api.Exeptions;
using System.Collections.Generic;

namespace RecetasMicroServiceTest
{
    [TestClass]
    public class UpdateRecetaCommandHandlerTests
    {

        private Mock<IRecetaRepository> _repositoryMock;
        private UpdateRecetaCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IRecetaRepository>();
            _handler = new UpdateRecetaCommandHandler(_repositoryMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public async Task Handle_WhenRecetaDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var request = new UpdateRecetaRequest { codigo = 1, Command = new UpdateRecetaCommand { estado = "Activo" } };
            _repositoryMock.Setup(r => r.GetById(1)).Returns((Receta)null); // Simula que no se encuentra la receta

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: La excepción KeyNotFoundException debería ser lanzada
        }

        [TestMethod]
        public async Task Handle_WhenRecetaExists_UpdatesRecetaSuccessfully()
        {
            // Arrange
            var receta = new Receta { codigo = 1, estado = "Pendiente" };
            var request = new UpdateRecetaRequest { codigo = 1, Command = new UpdateRecetaCommand { estado = "Activo" } };
            _repositoryMock.Setup(r => r.GetById(1)).Returns(receta); // Simula que la receta existe
            _repositoryMock.Setup(r => r.Update(It.IsAny<Receta>())).Verifiable(); // Verifica que el método Update se llama

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: Verifica que la receta fue actualizada
            _repositoryMock.Verify(r => r.Update(It.Is<Receta>(rec => rec.estado == "Activo")), Times.Once); // Se debe haber llamado a Update con el estado "Activo"
        }
    }
}

