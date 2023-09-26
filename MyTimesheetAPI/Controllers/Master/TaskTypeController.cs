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
    public class TaskTypeController : ControllerBase
    {
        private readonly ITaskTypeRepository taskTypeRepository;
        private readonly IMapper mapper;

        public TaskTypeController(ITaskTypeRepository taskTypeRepository, IMapper mapper)
        {
            this.taskTypeRepository = taskTypeRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTaskType()
        {
            var result = await taskTypeRepository.GetAllTaskType();
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetTaskTypeById(int Id)
        {
            var result = await taskTypeRepository.GetTaskTypeById(Id);
            if(result == null)
            {
                return NotFound();
            }
            var res = mapper.Map<TaskTypeEditDTO>(result);
            return Ok(res);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertTaskType(TaskTypeAddDTO taskTypeAddDTO)
        {
            TaskType taskType = mapper.Map<TaskType>(taskTypeAddDTO);
            var result = await taskTypeRepository.InsertTaskType(taskType);
            var res = mapper.Map<TaskTypeEditDTO>(result);
            return CreatedAtAction(nameof(GetTaskTypeById), new { id = taskType.Id }, res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTaskType(TaskTypeEditDTO taskTypeEditDTO)
        {
            var data = await taskTypeRepository.GetTaskTypeById(taskTypeEditDTO.Id);
            if(data == null)
            {
                return NotFound();
            }
            data = mapper.Map<TaskType>(taskTypeEditDTO);
            data.ModifiedOn = DateTime.Now;
            data.ModifiedBy = "Admin";
            data.ModifiedFrom = "::1";
            var result = await taskTypeRepository.UpdateTaskType(data);
            var res = mapper.Map<TaskTypeEditDTO> (result);
            return Ok(res);
        }
        [HttpDelete]
        [Route("{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTaskType(int Id)
        {
          var data = await taskTypeRepository.GetTaskTypeById(Id);
            if (data == null)
            {
                return NotFound();
            }
            data.IsDaleted = true;
            data.DeletedBy = "Admin";
            data.DeletedFrom = "::1";
            data.DeletedOn = DateTime.Now;
            var result = await taskTypeRepository.DeleteTaskType(data);
            return Ok(result);  
        }

    }
}
