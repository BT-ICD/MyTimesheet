using AutoMapper;
using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyTimesheetAPI.Controllers.Master
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;

        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllClient()
        {
            var result = await clientRepository.GetAllClientAsync();
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{clientId:int}")]
        public async Task<IActionResult> GetByClientId(int clientId)
        {
            var result = await clientRepository.GetClientById(clientId);
            if (result == null)
                return NotFound();
            var res = mapper.Map<ClientEditDTO>(result);
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertClient(ClientAddDTO clientAddDTO)
        {
            Client client = mapper.Map<Client>(clientAddDTO);
            var result = await clientRepository.InsertClient(client);
            var res = mapper.Map<ClientEditDTO>(result);
            return CreatedAtAction(nameof(GetByClientId), new { clientId = client.ClientId }, res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient(ClientEditDTO clientEditDTO)
        {
            var data = await clientRepository.GetClientById(clientEditDTO.ClientId);
            if (data == null)
            {
                return NotFound();
            }
            data = mapper.Map<Client>(clientEditDTO);
            data.ModifiedOn = DateTime.Now;
            data.ModifiedBy = "Admin";
            data.ModifiedFrom = "::1";
            var result = await clientRepository.UpdateClient(data);
            var res = mapper.Map<ClientEditDTO>(result);
            return Ok(res);
        }

        [HttpDelete]
        [Route("{clientId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteClient(int clientId)
        {
            var data = await clientRepository.GetClientById(clientId);
            if(data == null)
            {
                return NotFound();
            }
            data.IsDaleted = true;
            data.DeletedOn = DateTime.Now;
            data.DeletedBy = "Admin";
            data.DeletedFrom = "::1";
            var result  = await clientRepository.DeleteClient(data);
            return Ok(result);
        }
    }
}
