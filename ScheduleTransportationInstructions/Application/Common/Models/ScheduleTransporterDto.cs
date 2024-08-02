using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    /// <summary>
    /// Dto used to assign transporter to a product. 
    /// </summary>
    public class ScheduleTransporterDto
    {
        public int ScheduleTransportID { get; set; }
        public DateTime DateScheduled { get; set; }
        public string Transporter { get; set; }      
        public int InstructionId { get; set; }
        public int ProductId { get; set; }
    }
}
