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
    public class TaskTypeController : ControllerBase
    {
        private readonly ITaskTypeRepository taskTypeRepository;
        private readonly IMapper mapper;
        private readonly ILogger<TaskTypeController> logger;

        public TaskTypeController(ITaskTypeRepository taskTypeRepository, IMapper mapper, ILogger<TaskTypeController> logger)
        {
            this.taskTypeRepository = taskTypeRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.logger.LogDebug("NLog injected into TaskTypeController");

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTaskType()
        {
            try
            {
                logger.LogInformation("Request: GetAllTaskType");

                var result = await taskTypeRepository.GetAllTaskType();

                int Count = result.Count();

                logger.LogInformation($"Response: GetAllTaskType-TaskType Count: {Count}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetAllTaskType");
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetTaskTypeById(int Id)
        {
            try
            {
                string requestJson = JsonConvert.SerializeObject(Id);

                logger.LogInformation($"Request: GetTaskTypeById:{requestJson}");

                var result = await taskTypeRepository.GetTaskTypeById(Id);
                if (result == null)
                {
                    return NotFound();
                }
                var response = mapper.Map<TaskTypeEditDTO>(result);

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: GetTaskTypeById :{responseJson}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetTaskTypeById");
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertTaskType(TaskTypeAddDTO taskTypeAddDTO)
        {
            try
            {
                string requestJson = JsonConvert.SerializeObject(taskTypeAddDTO);

                logger.LogInformation($"Request: Insert TaskType:{requestJson}");

                TaskType taskType = mapper.Map<TaskType>(taskTypeAddDTO);
                var result = await taskTypeRepository.InsertTaskType(taskType);
                var response = mapper.Map<TaskTypeEditDTO>(result);

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: Inserted TaskType:{responseJson}");

                return CreatedAtAction(nameof(GetTaskTypeById), new { id = taskType.Id }, response);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error in Inserted TaskType");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
          
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskType(TaskTypeEditDTO taskTypeEditDTO)
        {
            try
            {
                var data = await taskTypeRepository.GetTaskTypeById(taskTypeEditDTO.Id);
                if (data == null)
                {
                    return NotFound();
                }
                string requestJson = JsonConvert.SerializeObject(taskTypeEditDTO);

                logger.LogInformation($"Request: Update TaskType: {requestJson}");

                data = mapper.Map<TaskType>(taskTypeEditDTO);
                data.ModifiedOn = DateTime.Now;
                data.ModifiedBy = "Admin";
                data.ModifiedFrom = "::1";
                var result = await taskTypeRepository.UpdateTaskType(data);
                var response = mapper.Map<TaskTypeEditDTO>(result);

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: Updated TaskType:{responseJson}");

                return Ok(response);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error in UpdateTaskType");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        [HttpDelete]
        [Route("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTaskType(int Id)
        {
            try
            {
                var data = await taskTypeRepository.GetTaskTypeById(Id);
                if (data == null)
                {
                    return NotFound();
                }
                string requestJson = JsonConvert.SerializeObject(data);

                logger.LogInformation($"Request: DeleteTaskType: {requestJson}");

                data.IsDaleted = true;
                data.DeletedBy = "Admin";
                data.DeletedFrom = "::1";
                data.DeletedOn = DateTime.Now;
                var result = await taskTypeRepository.DeleteTaskType(data);
                return Ok(result);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error in DeleteTaskType");
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }

    }
}
