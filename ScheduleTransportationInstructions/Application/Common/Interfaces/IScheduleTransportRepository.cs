using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IScheduleTransportRepository
    {
        Task<IEnumerable<GetScheduledInstructionDTO>> GetScheduleTransports();
        Task<GetScheduledInstructionDTO> GetScheduleTransportById(int id);
        Task<ScheduleTransport> AddScheduleTransport(ScheduleTransport scheduleTransport);
        Task<ScheduleTransport> UpdateScheduleTransport(ScheduleTransport scheduleTransport);
        Task<ScheduleTransport> DeleteScheduleTransport(int scheduleTransportId);
        Task<ScheduleTransport> InstructionExists(int instructionId);
    }
}
