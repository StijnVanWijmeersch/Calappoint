using Calappoint.Domain.Clients;
using Calappoint.Infrastructure.Database;

namespace Calappoint.Infrastructure.Clients;

internal class ClientRepository(CalappointDbContext context) : IClientRepository
{
    public void Insert(Client client)
    {
        context.Clients.Add(client);
    }
}
