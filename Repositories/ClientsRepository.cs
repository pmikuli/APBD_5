using Microsoft.EntityFrameworkCore;
using Zadanie7.Interfaces;
using Zadanie7.Models;
using Zadanie7.Models.DTOs;

namespace Zadanie7.Repositories
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly S24087Context _context;

        public ClientsRepository(S24087Context context)
        {
            _context = context;
        }

        public async Task DeleteClient(ClientDTO clientDTO)
        {
            var hasTrips = _context
                .Trips
                .Any(e =>
                    e
                    .ClientTrips
                    .Any(ct => ct.IdClientNavigation.FirstName == clientDTO.FirstName
                        && ct.IdClientNavigation.LastName == clientDTO.LastName)
                );

            if (hasTrips)
            {
                throw new Exception("Can't delete the client because they have related trips");
            }

            var client = await _context.Clients.Where(c => c.FirstName == clientDTO.FirstName && c.LastName == clientDTO.LastName).FirstOrDefaultAsync();
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }
    }
}
