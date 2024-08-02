using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CQRS.Instructions.Query
{
    public class GetInstructionsById : IRequest<GetInstructionDTO>
    {
        public int Id { get; set; }
    }

    public class GetInstructionsByIdHandler : IRequestHandler<GetInstructionsById, GetInstructionDTO>
    {
        private readonly IInstructionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInstructionsByIdHandler> _logger;

        public GetInstructionsByIdHandler(IInstructionRepository instructionRepository, IMapper mapper, ILogger<GetInstructionsByIdHandler> logger)
        {
            _repo = instructionRepository;
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Request handler for GetInstructionsById.
        /// </summary>
        /// <param name="query">The GetInstructionsById request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>returns GetInstructionDTO object on given id.</returns>
        public async Task<GetInstructionDTO> Handle(GetInstructionsById query, CancellationToken cancellationToken)
        {
            // Log the start of the request handling
            _logger.LogInformation("Handling GetInstructionsById request for ID: {Id}", query.Id);

            try
            {
                // Retrieve the instruction from the repository
                var instruction = await _repo.GetInstructionById(query.Id);

                if (instruction == null)
                {
                    // Log that no instruction was found for the given ID
                    _logger.LogInformation("No instruction found for ID: {Id}", query.Id);
                    return null;
                }

                // Map the retrieved instruction to a DTO
                var dto = _mapper.Map<GetInstructionDTO>(instruction);

                // Log the successful handling of the request
                _logger.LogInformation("Successfully handled GetInstructionsById request for ID: {Id}", query.Id);

                return dto;
            }
            catch (Exception ex)
            {
                // Log any exceptions that occur during request handling
                _logger.LogError(ex, "An error occurred while handling GetInstructionsById request for ID: {Id}", query.Id);

                // Rethrow the exception to be handled at a higher level or by a global exception handler
                throw;
            }
        }
    }
}
