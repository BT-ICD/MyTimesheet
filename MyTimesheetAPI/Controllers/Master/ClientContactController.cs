using AutoMapper;
using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;

namespace MyTimesheetAPI.Controllers.Master
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientContactController : ControllerBase
    {
        private readonly IClientContactRepository clientContactRepository;
        private readonly IMapper mapper;

        public ClientContactController(IClientContactRepository clientContactRepository, IMapper mapper)
        {
            this.clientContactRepository = clientContactRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllClientContact()
        {
            var result = await clientContactRepository.GetAllClientContact();
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{contactId:int}")]
        public async Task<IActionResult> GetClientContactById(int contactId)
        {
            var data = await clientContactRepository.GetClientContactById(contactId);
            if(data == null)
                return NotFound();
            var res = mapper.Map<ClientContactEditDTO>(data);
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertClientContact(ClientContactAddDTO clientContactAddDTO)
        {
            ClientContact clientContact = mapper.Map<ClientContact>(clientContactAddDTO);
            var result = await clientContactRepository.InsertClientContact(clientContact);
            var res = mapper.Map<ClientContactEditDTO>(result);
            return CreatedAtAction(nameof(GetClientContactById), new { contactId = clientContact.ContactId }, res);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateClientContact(ClientContactEditDTO clientContactEditDTO)
        {
            var data = await clientContactRepository.GetClientContactById(clientContactEditDTO.ContactId);
            if (data == null)
            {
                return NotFound();
            }
            data = mapper.Map<ClientContact>(clientContactEditDTO);
            data.ModifiedOn = DateTime.Now;
            data.ModifiedBy = "Admin";
            data.ModifiedFrom = "::1";      
            var result = await clientContactRepository.UpdateClientContact(data);
            var response = mapper.Map<ClientContactEditDTO>(result);
            return Ok(response);
        }

        [HttpDelete]
        [Route("{contactId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteClientContact(int contactId)
        {
            var data = await clientContactRepository.GetClientContactById(contactId);
            if (data == null)
            {
                return NotFound();
            }
            data.IsDaleted = true;
            data.DeletedOn = DateTime.Now;
            data.DeletedBy = "Admin";
            data.DeletedFrom = "::1";
            var result = await clientContactRepository.DeleteClientContact(data);
            return Ok(result);
        }
    }
}
