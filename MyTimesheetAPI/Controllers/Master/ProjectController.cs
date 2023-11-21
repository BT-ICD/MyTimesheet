using AutoMapper;
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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(IProjectRepository projectRepository, IMapper mapper, ILogger<ProjectController> logger)
        {
            this.projectRepository = projectRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProject()
        {
            var result = await projectRepository.GetAllProjectAsync();
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{projectId:int}")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            try
            {
                string requestJson = JsonConvert.SerializeObject(projectId);

                logger.LogInformation($"Request: GetTeamMemberById:{requestJson}");

                var result = await projectRepository.GetProjectById(projectId);
                if (result == null)
                    return NotFound();

                var response = mapper.Map<ProjectEditDTO>(result);

                string responseJson = JsonConvert.SerializeObject(response);
                logger.LogInformation($"Response: GetProjectById: {responseJson}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetProjectById");
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertProject(ProjectAddDTO projectAddDTO)
        {
            Project project = mapper.Map<Project>(projectAddDTO);
            var result = await projectRepository.InsertProject(project);

            var response = mapper.Map<ProjectEditDTO>(result);
            return CreatedAtAction(nameof(GetProjectById), new { projectId = project.ProjectId }, response);


        }
    }
}
