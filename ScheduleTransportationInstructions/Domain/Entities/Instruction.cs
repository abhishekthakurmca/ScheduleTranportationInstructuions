using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Instruction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime InstructionDate { get; set; } = DateTime.Now;
        //Auto populate but allow for changes

        [Required]
        public string ClientName { get; set; }
        //Select from a client list

        [Required]
        public string PickupAddress { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        //Client reference
        [Required]
        public string ClientRef { get; set; }

        [Required]
        public string BillingRef { get; set; }
        //Billing references are used to obtain charge rates

        [Required]
        public string Status { get; set; }
        //When instruction is captured, set status to “Pending”
       

        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ScheduleTransport> Transporter { get; set; }
    }
}
