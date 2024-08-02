using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        public decimal Qty { get; set; }

        [ForeignKey("Instruction")]
        public int InstructionId { get; set; }
        public Instruction? Instruction { get; set; }

        public IEnumerable<ScheduleTransport> Transporter { get; set; }
    }
}
