using AutoMapper;
using Azure;
using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Persistence.Repositories;

namespace MyTimesheetAPI.Controllers.Master
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientContactController : ControllerBase
    {
        private readonly IClientContactRepository clientContactRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ClientContactController> logger;
        private readonly IDesignationRepository designationRepository;

        public ClientContactController(IClientContactRepository clientContactRepository, IMapper mapper, ILogger<ClientContactController> logger, IDesignationRepository designationRepository)
        {
            this.clientContactRepository = clientContactRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.designationRepository = designationRepository;
            this.logger.LogDebug("NLog injected into ClientContactController");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllClientContact()
        {
            try
            {
                logger.LogInformation("Request: GetAllClientContacts");

                var result = await clientContactRepository.GetAllClientContact();

                int Count = result.Count();

                logger.LogInformation($"Response: GetAllClientContacts-ClientContact Count: {Count}");


                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetAllClientContactList");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{contactId:int}")]
        public async Task<IActionResult> GetClientContactById(int contactId)
        {
            try
            {
                string requestJson = JsonConvert.SerializeObject(contactId);

                logger.LogInformation($"Request: GetClientContactById:{requestJson}");


                var data = await clientContactRepository.GetClientContactById(contactId);
                if (data == null)
                    return NotFound();
                var response = mapper.Map<ClientContactEditDTO>(data);
                response.DesignationName = data.designation?.DesignationName;

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: GetClientContactById :{responseJson}");

                return Ok(response);

            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error in GetClientContactById");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertClientContact(ClientContactAddDTO clientContactAddDTO)
        {
            try
            {
                string requestJson = JsonConvert.SerializeObject(clientContactAddDTO);

                logger.LogInformation($"Request: Insert ClientContact:{requestJson}");

                //    var designationList = await designationRepository.GetAllDesignationAsync();

                //// Find selectedDesignation
                //    var selectedDesignation = designationList.FirstOrDefault(d => d.DesignationId == clientContactAddDTO.DesignationId);

                ClientContact clientContact = mapper.Map<ClientContact>(clientContactAddDTO);
                var result = await clientContactRepository.InsertClientContact(clientContact);
                var response = mapper.Map<ClientContactEditDTO>(result);
                response.DesignationName = result.designation?.DesignationName;

                //// Add DesignationName to the response without modifying the entity
                //    if (selectedDesignation != null)
                //    {
                //        response.DesignationName = selectedDesignation.DesignationName;
                //    }

                string responseJson = JsonConvert.SerializeObject(response);

                 logger.LogInformation($"Response: Inserted ClientContact:{responseJson}");

                return CreatedAtAction(nameof(GetClientContactById), new { contactId = clientContact.ContactId }, response);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error in Inserted ClientContact");
                    return StatusCode(StatusCodes.Status500InternalServerError);

                }

        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateClientContact(ClientContactEditDTO clientContactEditDTO)
        {
            try
            {
                var data = await clientContactRepository.GetClientContactById(clientContactEditDTO.ContactId);
                if (data == null)
                {
                    return NotFound();
                }
                string requestJson = JsonConvert.SerializeObject(clientContactEditDTO);

                logger.LogInformation($"Request: Update ClientContact: {requestJson}");

                data = mapper.Map<ClientContact>(clientContactEditDTO);
                data.ModifiedOn = DateTime.Now;
                data.ModifiedBy = "Admin";
                data.ModifiedFrom = "::1";
                var result = await clientContactRepository.UpdateClientContact(data);
                var response = mapper.Map<ClientContactEditDTO>(result);
                response.DesignationName = result.designation?.DesignationName;

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: Updated ClientContact:{responseJson}");


                return Ok(response);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error in UpdateClientContact");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{contactId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteClientContact(int contactId)
        {
            try
            {

                var data = await clientContactRepository.GetClientContactById(contactId);
                if (data == null)
                {
                    return NotFound();
                }
                string requestJson = JsonConvert.SerializeObject(data);

                logger.LogInformation($"Request: DeleteClientContact: {requestJson}");

                data.IsDaleted = true;
                data.DeletedOn = DateTime.Now;
                data.DeletedBy = "Admin";
                data.DeletedFrom = "::1";
                var result = await clientContactRepository.DeleteClientContact(data);

                string responseJson = JsonConvert.SerializeObject(result);

                logger.LogInformation($"Response: Deleted ClientContact: {responseJson}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in DeleteClientContact");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
