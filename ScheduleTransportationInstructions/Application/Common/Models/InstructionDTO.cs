using Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models
{
    /// <summary>
    /// Dto used to save new instruction with products 
    /// </summary>
    public class InstructionDTO
    {        
        public DateTime InstructionDate { get; set; } = DateTime.UtcNow;       
        public string ClientName { get; set; }            
        public string PickupAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public string ClientRef { get; set; }
        public string BillingRef { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
