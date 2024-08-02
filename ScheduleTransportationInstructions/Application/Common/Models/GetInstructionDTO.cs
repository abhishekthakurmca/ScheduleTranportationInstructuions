using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    /// <summary>
    /// Dto to get list of instructions with its products.
    /// </summary>
    public class GetInstructionDTO
    {
        public int Id { get; set; }
        public DateTime InstructionDate { get; set; }= DateTime.UtcNow;
        public string ClientName { get; set; }
        public string PickupAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string ClientRef { get; set; }
        public string BillingRef { get; set; }
        public string Status { get; set; }            
        public List<GetProductDTO> Products { get; set; }
    }
}
