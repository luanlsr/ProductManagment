using ProductManagment.Domain.Core.Base;
using ProductManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Domain.DTOs
{
    public class ProductDTO : EntityBaseDTO<Guid>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string SKU { get; set; }

        public Guid StockId { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
