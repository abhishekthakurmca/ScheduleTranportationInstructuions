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

namespace Application.Common.CQRS.Instructions.Commands
{
    public class UpdateInstructionCommand : GetInstructionDTO, IRequest<GetInstructionDTO>
    {
    }

    public class UpdateInstructionHandler : IRequestHandler<UpdateInstructionCommand, GetInstructionDTO>
    {
        private readonly IInstructionRepository _repo;
        private readonly IMapper _mapper;
        ILogger<UpdateInstructionHandler> _logger;
        public UpdateInstructionHandler(IInstructionRepository repo, IMapper mapper, ILogger<UpdateInstructionHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Request handler for UpdateInstructionCommand.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>returns updated InstructionDTO object.</returns>
        public async Task<GetInstructionDTO> Handle(UpdateInstructionCommand request, CancellationToken cancellationToken)
        {
            // Map the properties of the request object to an Instruction object
            var updatedInstructions = _mapper.Map<Instruction>(request);

            // Update the instructions in the repository
            var updateInstructions = await _repo.UpdateInstruction(updatedInstructions);

            // Log a message indicating that the instructions have been updated
            _logger.LogInformation($"Handling UpdateInstructionCommand {request}");

            // Map the updated instructions back to a GetInstructionDTO object
            var updatedInstructionDTO = _mapper.Map<GetInstructionDTO>(updateInstructions);

            // Return the updated instruction DTO
            return updatedInstructionDTO;
        }
    }
}
