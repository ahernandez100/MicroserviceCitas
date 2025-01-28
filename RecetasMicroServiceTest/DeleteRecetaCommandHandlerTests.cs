using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecetasServices.Application.Commans;
using RecetasServices.Domain.Entities;
using RecetasServices.Domain.Repositories;
using RecetasServices.Api.Exeptions;
using System.Threading;
using System.Threading.Tasks;

namespace RecetasMicroServiceTest
{

        [TestClass]
        public class DeleteRecetaCommandHandlerTests
        {
            private Mock<IRecetaRepository> _repositoryMock;
            private DeleteRecetaCommandHandler _handler;

            [TestInitialize]
            public void Setup()
            {
                _repositoryMock = new Mock<IRecetaRepository>();
                _handler = new DeleteRecetaCommandHandler(_repositoryMock.Object);
            }

            [TestMethod]
            [ExpectedException(typeof(NotFoundException))]
            public async Task Handle_WhenRecetaDoesNotExist_ThrowsNotFoundException()
            {
                // Arrange
                var request = new DeleteRecetaCommand { codigo = 1 };
                _repositoryMock.Setup(r => r.GetById(1)).Returns((Receta)null); // Simula que no se encuentra la receta

                // Act
                await _handler.Handle(request, CancellationToken.None);

                // Assert: La excepción NotFoundException debería ser lanzada
            }

            [TestMethod]
            public async Task Handle_WhenRecetaExists_ReturnsSuccessResult()
            {
                // Arrange
                var receta = new Receta { codigo = 1, nombrePaciente = "Juan Perez" };
                var request = new DeleteRecetaCommand { codigo = 1 };
                _repositoryMock.Setup(r => r.GetById(1)).Returns(receta); // Simula que la receta existe
                _repositoryMock.Setup(r => r.Delete(1)).Returns(1); // Simula que se eliminó correctamente y se afectó una fila

                // Act
                var result = await _handler.Handle(request, CancellationToken.None);

                // Assert: El valor retornado debe ser 1, lo que indica que una fila fue eliminada
                Assert.AreEqual(1, result);
            }
        }
    }

