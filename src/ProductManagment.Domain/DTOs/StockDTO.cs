using ProductManagment.Domain.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Domain.DTOs
{
    public class StockDTO : EntityBaseDTO<Guid>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
