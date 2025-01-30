using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Domain.Core.Base
{
    public abstract class EntityBaseDTO<TId>
    {
        public Guid Id { get; set; }
    }
}
