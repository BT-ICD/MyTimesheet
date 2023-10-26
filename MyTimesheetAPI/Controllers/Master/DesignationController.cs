using AutoMapper;
using Azure;
using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyTimesheetAPI.Controllers.Master
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationRepository designationRepository;  
        private readonly IMapper mapper;
        private readonly ILogger<DesignationController> logger;

        public DesignationController(IDesignationRepository designationRepository, IMapper mapper, ILogger<DesignationController> logger)
        {
            this.designationRepository = designationRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.logger.LogDebug("NLog injected into DesignationController");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                logger.LogInformation("Request: GetAllDesignations");

                var result = await designationRepository.GetAllDesignationAsync();
                //Following line commented for a learning refernce as mapper transform list to the list of DTO
                //var response = mapper.Map<List<DesignationEditDTO>>(result);
                int Count = result.Count();

                logger.LogInformation($"Response: GetAllDesignations-Designation Count: {Count}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetAllDesignationsList");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{designationId:int}")]
        public async Task<IActionResult> GetById(int designationId)
        {
            try
            {
                string requestJson = JsonConvert.SerializeObject(designationId);

                logger.LogInformation($"Request: GetDesignationById:{requestJson}");

                var result = await designationRepository.GetDesignationById(designationId);
                if (result == null)
                    return NotFound();

                var response = mapper.Map<DesignationEditDTO>(result);

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: GetDesignationById :{responseJson}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetDesignationById");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(DesignationAddDTO designationAddDTO)
        {
            try
            {
                string requestJson = JsonConvert.SerializeObject(designationAddDTO);

                logger.LogInformation($"Request: Insert Designation:{requestJson}");

                Designation designation = mapper.Map<Designation>(designationAddDTO);
                var result = await designationRepository.Add(designation);

                var response = mapper.Map<DesignationEditDTO>(result);

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: Inserted Designation:{responseJson}");

                return CreatedAtAction(nameof(GetById), new { designationId = designation.DesignationId }, response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in Inserted Designation");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
            
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(DesignationEditDTO designationEditDTO)
        {
            try
            {
                var data = await designationRepository.GetDesignationById(designationEditDTO.DesignationId);
                if (data == null)
                {
                    return NotFound();
                }
                string requestJson = JsonConvert.SerializeObject(designationEditDTO);

                logger.LogInformation($"Request: Update Designation: {requestJson}");

                data = mapper.Map<Designation>(designationEditDTO);
                //To-Do - next to replace with standard library to determine IP address from the request and User Name from the token
                data.ModifiedOn = DateTime.Now;
                data.ModifiedBy = "Admin";
                data.ModifiedFrom = "::1";
                var result = await designationRepository.Edit(data);
                var response = mapper.Map<DesignationEditDTO>(result);

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: Updated Designation:{responseJson}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in UpdateDesignation");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
        }

        [HttpDelete]
        [Route("{designationId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int designationId)
        {
            try
            {
                var data = await designationRepository.GetDesignationById(designationId);
                if (data == null)
                {
                    return NotFound();
                }
                string requestJson = JsonConvert.SerializeObject(data);

                logger.LogInformation($"Request: DeleteDesignation: {requestJson}");

                data.IsDaleted = true;
                data.DeletedOn = DateTime.Now;
                data.DeletedBy = "Admin";
                data.DeletedFrom = "::1";
                var result = await designationRepository.Delete(data);

                string responseJson = JsonConvert.SerializeObject(result);

                logger.LogInformation($"Response: Deleted designation: {responseJson}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in DeleteDesignation");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
          
        }
    }

