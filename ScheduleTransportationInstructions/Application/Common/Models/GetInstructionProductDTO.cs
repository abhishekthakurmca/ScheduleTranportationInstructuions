using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    /// <summary>
    /// Dto used in GetScheduledInstructionDTO to get list and a single instruction with its product and its transporter.
    /// </summary>
    public class GetInstructionProductDTO
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public decimal Qty { get; set; }
        public int? ScheduleTransportID { get; set; }
        public DateTime? DateScheduled { get; set; }
        public string? Transporter { get; set; }
    }
}
