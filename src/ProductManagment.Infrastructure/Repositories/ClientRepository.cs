using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.Repositories;
using ProductManagment.Infrastructure.Context;

namespace ProductManagment.Infrastructure.Repositories
{
    public class ClientRepository : Repository<Client, Guid>, IClientRepository
    {
        public ClientRepository(AppDbContext context) : base(context)
        {
        }
    }
}
