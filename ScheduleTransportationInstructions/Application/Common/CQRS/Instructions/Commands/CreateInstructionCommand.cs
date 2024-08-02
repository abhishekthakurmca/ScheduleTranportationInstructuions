using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CQRS.Instructions.Commands
{
    public class CreateInstructionCommand : InstructionDTO,  IRequest<GetInstructionDTO>
    {
    }

    public class CreateInstructionHandler : IRequestHandler<CreateInstructionCommand, GetInstructionDTO>
    {
        private readonly IInstructionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateInstructionHandler> _logger;
        public CreateInstructionHandler(IInstructionRepository repo, IMapper mapper, ILogger<CreateInstructionHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Request handler for CreateInstructionCommand.
        /// </summary>
        /// <param name="request">The CreateInstructionCommand request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task representing the asynchronous operation that returns a GetInstructionDTO object.</returns>
        public async Task<GetInstructionDTO> Handle(CreateInstructionCommand request, CancellationToken cancellationToken)
        {
            // Map the request to an Instruction object
            var newInstructions = _mapper.Map<Instruction>(request);

            // Log that the CreateInstructionCommand is being handled
            _logger.LogInformation($"Handle CreateInstructionCommand {request}");

            // Add the new instruction to the repository
            var createInstructions = await _repo.AddInstruction(newInstructions);

            // Map the added instruction back to a DTO
            var addedInstructionDTO = _mapper.Map<GetInstructionDTO>(createInstructions);

            return addedInstructionDTO;
        }
    }
}
