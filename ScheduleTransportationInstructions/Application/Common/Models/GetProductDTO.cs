using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    /// <summary>
    /// Dto used in GetInstructionDTO to get list and a single instruction with its product
    /// </summary>
    public class GetProductDTO
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public decimal Qty { get; set; }
    }
}
