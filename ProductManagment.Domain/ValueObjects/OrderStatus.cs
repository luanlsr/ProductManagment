using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Domain.ValueObjects
{
    public enum OrderStatus
    {
        Created,
        Paid,
        Shipped,
        Delivered
    }
}
