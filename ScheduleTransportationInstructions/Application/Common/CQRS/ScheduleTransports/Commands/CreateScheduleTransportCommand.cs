using Application.Common.CQRS.Instructions.Query;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CQRS.ScheduleTransports.Commands
{
    public class CreateScheduleTransportCommand : ScheduleTransporterDto, IRequest<GetScheduleTransportDTO>
    {

    }

    public class CreateScheduleTransportsHandler : IRequestHandler<CreateScheduleTransportCommand, GetScheduleTransportDTO>
    {
        private readonly IScheduleTransportRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateScheduleTransportsHandler> _logger;

        public CreateScheduleTransportsHandler(IScheduleTransportRepository repo, IMapper mapper,ILogger<CreateScheduleTransportsHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Request handler for CreateScheduleTransportCommand.
        /// </summary>
        /// <param name="request">The CreateScheduleTransportCommand request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task representing the asynchronous operation that returns a GetScheduleTransportDTO object.</returns>
        public async Task<GetScheduleTransportDTO> Handle(CreateScheduleTransportCommand request, CancellationToken cancellationToken)
        {
            // If both instruction and product don't exist, continue with the rest of logic or return back .
            var checkInstructionId = await _repo.InstructionExists(request.InstructionId);
            if (checkInstructionId != null)
            {
                return null;
            }

            var checkProductId = await _repo.InstructionExists(request.ProductId);
            if (checkProductId != null)
            {
                return null;
            }

            // Map the CreateScheduleTransportCommand to a ScheduleTransport entity using AutoMapper.
            var newSchedule = _mapper.Map<ScheduleTransport>(request);

            // Add the newly created schedule to the repository and await the result.
            var addSchedule = await _repo.AddScheduleTransport(newSchedule);

            // Log that the CreateScheduleTransportCommand is being handled
            _logger.LogInformation($"Handle CreateScheduleTransportCommand {request}");

            // Map the added schedule back to GetScheduleTransportDTO using AutoMapper.
            var addedScheduleDTO = _mapper.Map<GetScheduleTransportDTO>(addSchedule);

            // Return the DTO representing the added schedule.
            return addedScheduleDTO;
        }
    }
}
