using Zadanie7.Interfaces;
using Zadanie7.Models;
using Zadanie7.Models.DTOs;

namespace Zadanie7.Repositories
{
    public class TripsRepository : ITripsRepository
    {
        private readonly S24087Context _context;

        public TripsRepository(S24087Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TripDTO>> GetTripsAsync()
        {
            var result = _context
                .Trips
                .Select(e =>
                new TripDTO
                {
                    Name = e.Name,
                    Description = e.Description,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    MaxPeople = e.MaxPeople,
                    Countries = e.IdCountries
                        .Select(x => new CountryDTO { Name = x.Name }),
                    Clients = e.ClientTrips
                        .Select(e => new ClientDTO { FirstName = e.IdClientNavigation.FirstName, LastName = e.IdClientNavigation.LastName})
                }).ToList();

            return result;
        }
    }
}
