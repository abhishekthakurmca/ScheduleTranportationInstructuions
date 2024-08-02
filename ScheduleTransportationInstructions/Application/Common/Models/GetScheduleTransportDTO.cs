using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    public class GetScheduleTransportDTO
    {
        public int ScheduleTransportID { get; set; }
        public DateTime DateScheduled { get; set; }= DateTime.UtcNow;
        public string Transporter { get; set; }
        public GetInstructionDTO Instruction { get; set; }
        public GetProductDTO Product { get; set; }
    }
}
