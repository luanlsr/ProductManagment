using ProductManagment.Domain.Core.Base;
using ProductManagment.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Domain.DTOs
{
    public class OrderDTO : EntityBaseDTO<Guid>
    {
        public OrderStatus Status { get; set; }
    }
}
