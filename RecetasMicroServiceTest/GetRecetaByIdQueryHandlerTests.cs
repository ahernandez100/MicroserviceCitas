using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecetasServices.Application.Queries;
using RecetasServices.Domain.Entities;
using RecetasServices.Domain.Repositories;
using RecetasServices.Api.Exeptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecetasMicroServiceTest
{

    [TestClass]
    public class GetRecetaByIdQueryHandlerTests
    {
        private Mock<IRecetaRepository> _repositoryMock;
        private GetRecetaByIdQueryHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IRecetaRepository>();
            _handler = new GetRecetaByIdQueryHandler(_repositoryMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenRecetaDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var request = new GetRecetaByIdQuery { codigo = 1 }; // Código de receta que no existe
            _repositoryMock.Setup(r => r.GetById(1)).Returns((Receta)null); // Devuelve null, simulando que la receta no existe

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: La excepción NotFoundException debería ser lanzada
        }

        [TestMethod]
        public async Task Handle_WhenRecetaExists_ReturnsReceta()
        {
            // Arrange
            var receta = new Receta
            {
                codigo = 1,
                codigoPaciente = 1,
                nombrePaciente = "Omar Perez",
                codigoMedico = 1,
                nombreMedico="Carlos Rueda",
                fecha = DateTime.Now,
                observacion="Gripa",
                codigoCita=6,
                estado= "Activa",
            };

            var request = new GetRecetaByIdQuery { codigo = 1 };
            _repositoryMock.Setup(r => r.GetById(1)).Returns(receta); // Simula que la receta existe

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result); // Verifica que se devuelva una receta
            Assert.AreEqual(1, result.codigo); // Verifica que el código de la receta sea 1
            Assert.AreEqual(1, result.codigoPaciente); // Verifica que el codigoPaciente de la receta sea correcto
        }
    }
}
