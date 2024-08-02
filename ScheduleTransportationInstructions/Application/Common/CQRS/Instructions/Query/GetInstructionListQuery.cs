using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Application.Common.CQRS.Instructions.Query
{
    public class GetInstructionListQuery : IRequest<IEnumerable<GetInstructionDTO>>
    {
    }

    public class GetInstructionListHandler : IRequestHandler<GetInstructionListQuery, IEnumerable<GetInstructionDTO>>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInstructionListHandler> _logger;
        public GetInstructionListHandler(IInstructionRepository instructionRepository,IMapper mapper, ILogger<GetInstructionListHandler> logger)
        {
            _instructionRepository = instructionRepository;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Handles the GetInstructionListQuery request and returns a list of GetInstructionDTO objects.
        /// </summary>
        /// <param name="request">The GetInstructionListQuery request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation that returns a list of GetInstructionDTO objects.</returns>
        public async Task<IEnumerable<GetInstructionDTO>> Handle(GetInstructionListQuery request, CancellationToken cancellationToken)
        {
            // Log that we are calling the repository method from Mediator
            _logger.LogInformation("Calling Repository method GetInstructions()");

            // Retrieve instructions from the repository and map them to DTOs
            var instructions = await _instructionRepository.GetInstructions();
            var dtos = _mapper.Map<IEnumerable<GetInstructionDTO>>(instructions);

            return dtos;
        }
    }
}


