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

namespace Application.Common.CQRS.Instructions.Commands
{
    public class DeleteInstructionCommand : IRequest<InstructionDTO>
    {
        public int Id { get; set; }
    }

    public class DeleteInstructionHandler : IRequestHandler<DeleteInstructionCommand, InstructionDTO>
    {
        private readonly IInstructionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteInstructionHandler> _logger;
        public DeleteInstructionHandler(IInstructionRepository repo, IMapper mapper, ILogger<DeleteInstructionHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Request handler for DeleteInstructionCommand.
        /// </summary>
        /// <param name="request">The DeleteInstructionCommand request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>returns deleted InstructionDTO object.</returns>
        public async Task<InstructionDTO> Handle(DeleteInstructionCommand request, CancellationToken cancellationToken)
        {
            // Get the instruction details from the repository based on the provided ID
            var instructionDetails = await _repo.GetInstructionById(request.Id);

            // If no instruction details are found, return null
            if (instructionDetails == null)
            {
                _logger.LogInformation($"No instruction found with ID {request.Id}");
                return null;
            }

            // Log that the DeleteInstructionCommand is being handled
            _logger.LogInformation($"Handling DeleteInstructionCommand {request}");

            // Map the request to an Instruction object
            var mapInstruction = _mapper.Map<Instruction>(request);

            // Delete the instruction from the repository
            var deleteInstructions = await _repo.DeleteInstruction(mapInstruction.Id);

            // Map the deleted instruction back to a DTO
            var deletedInstructionDTO = _mapper.Map<InstructionDTO>(deleteInstructions);

            return deletedInstructionDTO;
        }
    }
}
