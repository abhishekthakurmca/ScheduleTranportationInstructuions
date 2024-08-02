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
    public class GetScheduleTransportListQuery: IRequest<IEnumerable<GetScheduledInstructionDTO>>
    {
    }
    public class GetScheduleTransportListHandler : IRequestHandler<GetScheduleTransportListQuery, IEnumerable<GetScheduledInstructionDTO>>
    {
        private readonly IScheduleTransportRepository _repo;

        private readonly IMapper _mapper;
        private readonly ILogger<GetScheduleTransportListHandler> _logger;
        public GetScheduleTransportListHandler(IScheduleTransportRepository repo, IMapper mapper, ILogger<GetScheduleTransportListHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Handles the GetScheduleTransportListQuery request and returns a list of GetInstructionDTO objects.
        /// </summary>
        /// <param name="request">The GetScheduleTransportListQuery request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation that returns a list of GetScheduledInstructionDTO objects.</returns>
        public async Task<IEnumerable<GetScheduledInstructionDTO>> Handle(GetScheduleTransportListQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<GetScheduledInstructionDTO>>(await _repo.GetScheduleTransports());
        }
    }
}
