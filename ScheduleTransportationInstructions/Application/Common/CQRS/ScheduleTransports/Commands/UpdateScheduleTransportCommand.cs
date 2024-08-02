using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CQRS.ScheduleTransports.Commands
{
    public class UpdateScheduleTransportCommand: ScheduleTransporterDto,IRequest<GetScheduleTransportDTO>
    {

    }

    public class UpdateScheduleTransportsHandler : IRequestHandler<UpdateScheduleTransportCommand, GetScheduleTransportDTO>
    {
        private readonly IScheduleTransportRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateScheduleTransportsHandler> _logger;
        public UpdateScheduleTransportsHandler(IScheduleTransportRepository repo, IMapper mapper, ILogger<UpdateScheduleTransportsHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;

        }

        /// <summary>
        /// Request handler for UpdateScheduleTransportCommand.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>returns updated GetScheduleTransportDTO object.</returns>
        public async Task<GetScheduleTransportDTO> Handle(UpdateScheduleTransportCommand request, CancellationToken cancellationToken)
        {
            // Map the UpdateScheduleTransportCommand to a ScheduleTransport entity using AutoMapper.
            var updatedSchedule = _mapper.Map<ScheduleTransport>(request);

            // Update the schedule in the repository and await the result.
            var updateSchedule = await _repo.UpdateScheduleTransport(updatedSchedule);

            // Log a message indicating that the ScheduleTransport have been updated
            _logger.LogInformation($"Handling UpdateScheduleTransportCommand {request}");

            // Map the updated schedule back to GetScheduleTransportDTO using AutoMapper.
            var updatedScheduleDTO = _mapper.Map<GetScheduleTransportDTO>(updateSchedule);

            // Return the DTO representing the updated schedule.
            return updatedScheduleDTO;
        }
    }
}
