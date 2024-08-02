using Application.Common.CQRS.ScheduleTransports.Commands;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.CQRS.ScheduleTransports.Querys
{
    public class GetScheduleTransportById:IRequest<GetScheduledInstructionDTO>
    {
        public int ScheduleTransportID { get; set; }
    }

    public class GetScheduleTransportByIdHandler : IRequestHandler<GetScheduleTransportById, GetScheduledInstructionDTO>
    {
        private readonly IScheduleTransportRepository _repo;

        private readonly IMapper _mapper;
        private readonly ILogger<GetScheduleTransportByIdHandler> _logger;
        public GetScheduleTransportByIdHandler(IScheduleTransportRepository repo, IMapper mapper, ILogger<GetScheduleTransportByIdHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Request handler for GetScheduleTransportById.
        /// </summary>
        /// <param name="query">The GetScheduleTransportById request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>returns GetScheduledInstructionDTO object on given id.</returns>
        public async Task<GetScheduledInstructionDTO> Handle(GetScheduleTransportById request, CancellationToken cancellationToken)
        {

            // Log the start of the request handling
            _logger.LogInformation("Handling GetScheduleTransportById request for ID: {Id}", request.ScheduleTransportID);

            var getScheduleById = _mapper.Map<GetScheduledInstructionDTO>(await _repo.GetScheduleTransportById(request.ScheduleTransportID));

            // Log the successful handling of the request
            _logger.LogInformation("Successfully handled GetScheduleTransportById request for ID: {Id}", request.ScheduleTransportID);


            if (getScheduleById == null) return null;
            return getScheduleById;
        }
    }
}
