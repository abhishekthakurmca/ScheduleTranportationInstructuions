using Application.Common.CQRS.Instructions.Query;
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
    public class DeleteScheduleTransportCommand:IRequest<ScheduleTransporterDto>
    {
        public int ScheduleTransportID { get; set; }
    }

    public class DeleteScheduleTransportHandler:IRequestHandler<DeleteScheduleTransportCommand, ScheduleTransporterDto>
    {
        private readonly IScheduleTransportRepository _repo;
        private readonly IInstructionRepository _repos;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteScheduleTransportHandler> _logger;
        public DeleteScheduleTransportHandler(IScheduleTransportRepository repo, IMapper mapper, IInstructionRepository repos, ILogger<DeleteScheduleTransportHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _repos = repos;
            _logger = logger;
        }
        /// <summary>
        /// Request handler for DeleteScheduleTransportCommand.
        /// </summary>
        /// <param name="request">The DeleteScheduleTransportCommand request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>returns deleted ScheduleTransporterDto object.</returns>
        public async Task<ScheduleTransporterDto> Handle(DeleteScheduleTransportCommand request, CancellationToken cancellationToken)
        {
            var instructionDetails = await _repo.GetScheduleTransportById(request.ScheduleTransportID);
            if (instructionDetails == null)
                return null;

            var instruction = await _repos.GetInstructionById(instructionDetails.Id);
             await _repos.DeleteInstruction(instruction.Id);

            // Log a message indicating that the ScheduleTransport have been updated
            _logger.LogInformation($"Handling DeleteScheduleTransportCommand {request}");

            var newInstructions = _mapper.Map<ScheduleTransport>(instructionDetails);
            var data = _repo.DeleteScheduleTransport(newInstructions.ScheduleTransportID);
            var addedInstructionDTO = _mapper.Map<ScheduleTransporterDto>(data); // Map back to DTO
            return addedInstructionDTO;
        }
    }
}
