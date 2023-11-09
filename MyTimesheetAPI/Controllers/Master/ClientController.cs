using AutoMapper;
using Azure;
using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyTimesheetAPI.Controllers.Master
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ClientController> logger;

        public ClientController(IClientRepository clientRepository, IMapper mapper, ILogger<ClientController> logger)
        {
            this.clientRepository = clientRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.logger.LogDebug("NLog injected into ClientController");

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllClient()
        {
            try
            {
                logger.LogInformation("Request: GetAllClients");

                var result = await clientRepository.GetAllClientAsync();
                
                int Count = result.Count();

                logger.LogInformation($"Response: GetAllClients-Client Count: {Count}");

                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error in GetAllClientList");
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("lookup")]
        public async Task<IActionResult> GetClientLookup()
        {
            try
            {
                logger.LogInformation("Request: GetClientLookup");

                var result = await clientRepository.GetClientLookupAsync();

                logger.LogInformation($"Response: GetClientLookup");

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetClientLookup");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{clientId:int}")]
        public async Task<IActionResult> GetByClientId(int clientId)
        {
            try
            {
                string requestJson = JsonConvert.SerializeObject(clientId);

                logger.LogInformation($"Request: GetClientById:{requestJson}");

                var result = await clientRepository.GetClientById(clientId);
                if (result == null)
                    return NotFound();
                var response = mapper.Map<ClientEditDTO>(result);

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: GetClientContactById :{responseJson}");


                return Ok(response);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error in GetClientById");
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertClient(ClientAddDTO clientAddDTO)
        {
            try
            {
                string requestJson = JsonConvert.SerializeObject(clientAddDTO);

                logger.LogInformation($"Request: Insert Client:{requestJson}");

                Client client = mapper.Map<Client>(clientAddDTO);
                var result = await clientRepository.InsertClient(client);
                var response = mapper.Map<ClientEditDTO>(result);

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: Inserted Client:{responseJson}");

                return CreatedAtAction(nameof(GetByClientId), new { clientId = client.ClientId }, response);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error in Inserted Client");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateClient(ClientEditDTO clientEditDTO)
        {
            try
            {

                var data = await clientRepository.GetClientById(clientEditDTO.ClientId);
                if (data == null)
                {
                    return NotFound();
                }
                string requestJson = JsonConvert.SerializeObject(clientEditDTO);

                logger.LogInformation($"Request: Update Client: {requestJson}");

                data = mapper.Map<Client>(clientEditDTO);
                data.ModifiedOn = DateTime.Now;
                data.ModifiedBy = "Admin";
                data.ModifiedFrom = "::1";
                var result = await clientRepository.UpdateClient(data);
                var response = mapper.Map<ClientEditDTO>(result);

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: Updated Client:{responseJson}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in UpdateClient");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpDelete]
        [Route("{clientId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteClient(int clientId)
        {
            try
            {
                var data = await clientRepository.GetClientById(clientId);
                if (data == null)
                {
                    return NotFound();
                }
                string requestJson = JsonConvert.SerializeObject(data);

                logger.LogInformation($"Request: DeleteClient: {requestJson}");

                data.IsDaleted = true;
                data.DeletedOn = DateTime.Now;
                data.DeletedBy = "Admin";
                data.DeletedFrom = "::1";
                var result = await clientRepository.DeleteClient(data);

                string responseJson = JsonConvert.SerializeObject(result);

                logger.LogInformation($"Response: Deleted Client: {responseJson}");

                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error in DeleteClient");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }
    }
}
