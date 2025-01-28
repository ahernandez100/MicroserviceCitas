using CitasMicroService.Domain.Entities;
using CitasMicroService.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using CitasMicroService.Application.Servicio;

namespace CitasMicroService.Application.Commans
{
    public class FinishCitaCommandHandler : IRequestHandler<FinishCitaRequest, Unit>
    {
        private readonly ICitaRepository _repository;
        private readonly IRabbitMQSender _rabbitMqSender;
        public FinishCitaCommandHandler(ICitaRepository repository, IRabbitMQSender rabbitMqSender)
        {
            _repository = repository;
            _rabbitMqSender = rabbitMqSender;
        }

        public Task<Unit> Handle(FinishCitaRequest request, CancellationToken cancellationToken)
        {
            //Busca la cita en la base de datos
            Cita cita = _repository.GetById(request.codigo);
            if (cita == null)
            {
                throw new KeyNotFoundException($"No se encontró una cita con el codigo {request.codigo}");
            }
            cita.estado = "Finalizada";
            _repository.Update(cita);
            // Enviar mensaje a RabbitMQ
            _rabbitMqSender.SendMessage(request);
            return Task.FromResult(Unit.Value); // Retorna un valor vacío (indica éxito
        }
    }
}