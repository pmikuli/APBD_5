using Microsoft.AspNetCore.Mvc;
using Zadanie7.Interfaces;
using Zadanie7.Models.DTOs;

namespace Zadanie7.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientsRepository _clientsRepository;

        public ClientController(IClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteClient(ClientDTO client)
        {
            try
            {
                await _clientsRepository.DeleteClient(client);
                return Ok();
            } catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }
    }
}
