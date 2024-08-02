using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ScheduleTransport
    {
        [Key]
        public int ScheduleTransportID { get; set; }
        [Required]
        public DateTime DateScheduled { get; set; } = DateTime.Now;
        //Auto populate but allow for changes
        [Required]
        public string Transporter { get; set; }
        //Select from a list of transporters
        [ForeignKey("Instruction")]
        public int InstructionId { get; set; }
        public Instruction? Instruction { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
