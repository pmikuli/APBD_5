using Zadanie7.Models.DTOs;

namespace Zadanie7.Interfaces
{
    public interface ITripsRepository
    {
        Task<IEnumerable<TripDTO>> GetTripsAsync();
    }
}
