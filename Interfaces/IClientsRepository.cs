using Zadanie7.Models.DTOs;

namespace Zadanie7.Interfaces
{
    public interface IClientsRepository
    {
        Task DeleteClient(ClientDTO clientDTO);
    }
}
