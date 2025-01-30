using ProductManagment.Domain.Core.Interface;
using ProductManagment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
    }
}
