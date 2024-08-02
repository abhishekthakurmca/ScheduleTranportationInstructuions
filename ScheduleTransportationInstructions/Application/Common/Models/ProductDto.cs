using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Models
{
    /// <summary>
    /// Dto used in InstructionDTO to save instruction with its product
    /// </summary>
    public class ProductDto
    {
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public decimal Qty { get; set; }
    }
}
