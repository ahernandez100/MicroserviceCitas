using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecetasServices.Application.Commans;
using RecetasServices.Application.DTO;
using RecetasServices.Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RecetasServices.Application.Commans;
using RecetasServices.Application.DTO;
using RecetasServices.Domain.Entities;
using RecetasServices.Domain.Repositories;
using RecetasServices.Application.Services;
using RecetasServices.Api.Exeptions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecetasMicroServiceTest
{

    [TestClass]
    public class AddRecetaCommandHandlerTests
    {
        private Mock<IRecetaRepository> _repositoryMock;
        private Mock<IPersonaService> _personaServiceMock;
        private Mock<ICitaService> _citaServiceMock;
        private AddRecetaCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IRecetaRepository>();
            _personaServiceMock = new Mock<IPersonaService>();
            _citaServiceMock = new Mock<ICitaService>();
            _handler = new AddRecetaCommandHandler(
                _repositoryMock.Object,
                _personaServiceMock.Object,
                _citaServiceMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenPacienteDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var request = new AddRecetaRequest { Commnad = new AddRecetaCommand { codigoPaciente = 1 } };
            _personaServiceMock.Setup(ps => ps.GetById(1, It.IsAny<string>())).Returns((PersonaDto)null);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: La excepción NotFoundException debería ser lanzada
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenPacienteIsNotAPaciente_ThrowsNotFoundException()
        {
            // Arrange
            var paciente = new PersonaDto { codigoTipoPersona = 1 }; // Tipo 1 no es paciente
            var request = new AddRecetaRequest { Commnad = new AddRecetaCommand { codigoPaciente = 1 } };
            _personaServiceMock.Setup(ps => ps.GetById(1, It.IsAny<string>())).Returns(paciente);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: La excepción NotFoundException debería ser lanzada
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenMedicoDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var request = new AddRecetaRequest { Commnad = new AddRecetaCommand { codigoMedico = 1 } };
            _personaServiceMock.Setup(ps => ps.GetById(1, It.IsAny<string>())).Returns((PersonaDto)null);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: La excepción NotFoundException debería ser lanzada
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenMedicoIsNotAMedico_ThrowsNotFoundException()
        {
            // Arrange
            var medico = new PersonaDto { codigoTipoPersona = 2 }; // Tipo 2 no es médico
            var request = new AddRecetaRequest { Commnad = new AddRecetaCommand { codigoMedico = 1 } };
            _personaServiceMock.Setup(ps => ps.GetById(1, It.IsAny<string>())).Returns(medico);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: La excepción NotFoundException debería ser lanzada
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenCitaDoesNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var request = new AddRecetaRequest { Commnad = new AddRecetaCommand { codigoCita = 1 } };
            _citaServiceMock.Setup(cs => cs.GetById(1, It.IsAny<string>())).Returns((CitaDto)null);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: La excepción NotFoundException debería ser lanzada
        }

    }
}

