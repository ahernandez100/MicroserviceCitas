using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CitasMicroService.Application.Commans;
using CitasMicroService.Domain.Entities;
using CitasMicroService.Domain.Repositories;
using CitasMicroService.Application.Servicio;
using CitasMicroService.Api.Exeptions;
using System;
using System.Threading;
using System.Threading.Tasks;
using CitasMicroService.Application.DTO;

namespace CitasMicroServiceTest
{
    [TestClass]
    public class AddCitaCommandHandlerTests
    {
        private Mock<ICitaRepository> _repositoryMock;
        private Mock<IPersonaService> _personaServiceMock;
        private AddCitaCommandHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<ICitaRepository>();
            _personaServiceMock = new Mock<IPersonaService>();
            _handler = new AddCitaCommandHandler(_repositoryMock.Object, _personaServiceMock.Object);
        }

        [TestMethod]
        public async Task Handle_WhenPacienteAndMedicoAreValid_AddsCitaSuccessfully()
        {
            // Arrange
            var request = new AddCitaRequest
            {
                Commnad = new AddCitaCommand
                {
                    codigoPaciente = 1,
                    codigoMedico = 2,
                    fecha = DateTime.Now,
                    lugar = "Hospital Central",
                    estado = "Pendiente"
                },
                token = "validToken"
            };

            var paciente = new PersonaDto
            {
                codigo = 1,
                nombres = "Juan",
                apellidos = "Perez",
                codigoTipoPersona = 2
            };

            var medico = new PersonaDto
            {
                codigo = 2,
                nombres = "Dr. Carlos",
                apellidos = "Mendoza",
                codigoTipoPersona = 1
            };

            _personaServiceMock.Setup(s => s.GetById(1, "validToken")).Returns(paciente);
            _personaServiceMock.Setup(s => s.GetById(2, "validToken")).Returns(medico);
            _repositoryMock.Setup(r => r.Add(It.IsAny<Cita>())).Returns(1);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.AreEqual(1, result);
            _repositoryMock.Verify(r => r.Add(It.Is<Cita>(c =>
                c.codigoPaciente == 1 &&
                c.codigoMedico == 2 &&
                c.lugar == "Hospital Central" &&
                c.estado == "Pendiente"
            )), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenPacienteNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var request = new AddCitaRequest
            {
                Commnad = new AddCitaCommand
                {
                    codigoPaciente = 999, // Paciente no existe
                    codigoMedico = 2,
                    fecha = DateTime.Now,
                    lugar = "Hospital Central",
                    estado = "Pendiente"
                },
                token = "validToken"
            };

            _personaServiceMock.Setup(s => s.GetById(999, "validToken")).Returns((PersonaDto)null);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: No es necesario, ya que ExpectedException maneja la validación
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenPacienteIsNotTypePaciente_ThrowsNotFoundException()
        {
            // Arrange
            var request = new AddCitaRequest
            {
                Commnad = new AddCitaCommand
                {
                    codigoPaciente = 1,
                    codigoMedico = 2,
                    fecha = DateTime.Now,
                    lugar = "Hospital Central",
                    estado = "Pendiente"
                },
                token = "validToken"
            };

            var paciente = new PersonaDto
            {
                codigo = 1,
                nombres = "Juan",
                apellidos = "Perez",
                codigoTipoPersona = 1 // No es paciente (no tipo 2)
            };

            _personaServiceMock.Setup(s => s.GetById(1, "validToken")).Returns(paciente);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: No es necesario, ya que ExpectedException maneja la validación
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenMedicoNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var request = new AddCitaRequest
            {
                Commnad = new AddCitaCommand
                {
                    codigoPaciente = 1,
                    codigoMedico = 999, // Médico no existe
                    fecha = DateTime.Now,
                    lugar = "Hospital Central",
                    estado = "Pendiente"
                },
                token = "validToken"
            };

            _personaServiceMock.Setup(s => s.GetById(999, "validToken")).Returns((PersonaDto)null);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: No es necesario, ya que ExpectedException maneja la validación
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public async Task Handle_WhenMedicoIsNotTypeMedico_ThrowsNotFoundException()
        {
            // Arrange
            var request = new AddCitaRequest
            {
                Commnad = new AddCitaCommand
                {
                    codigoPaciente = 1,
                    codigoMedico = 2,
                    fecha = DateTime.Now,
                    lugar = "Hospital Central",
                    estado = "Pendiente"
                },
                token = "validToken"
            };

            var medico = new PersonaDto
            {
                codigo = 2,
                nombres = "Carlos",
                apellidos = "Mendoza",
                codigoTipoPersona = 1 // No es médico (no tipo 2)
            };

            _personaServiceMock.Setup(s => s.GetById(2, "validToken")).Returns(medico);

            // Act
            await _handler.Handle(request, CancellationToken.None);

            // Assert: No es necesario, ya que ExpectedException maneja la validación
        }
    }
}
