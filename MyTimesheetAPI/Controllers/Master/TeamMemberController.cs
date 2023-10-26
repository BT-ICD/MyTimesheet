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
    public class TeamMemberController : ControllerBase
    {
        private readonly ITeamMemberRepository teamMemberRepository;
        private readonly IMapper mapper;
        private readonly ILogger<TeamMemberController> logger;

        public TeamMemberController(ITeamMemberRepository teamMemberRepository, IMapper mapper, ILogger<TeamMemberController> logger)
        {
            this.teamMemberRepository = teamMemberRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.logger.LogDebug("NLog injected into TeamMemberController");
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTeamMember()
        {
            try
            {
                logger.LogInformation("Request: GetAllTeamMember");
                var result = await teamMemberRepository.GetAllTeamMemberAsync();
                int Count = result.Count();

                logger.LogInformation($"Response: GetAllTeamMember- TeamMember Count: {Count}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetAllTeamMember");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{teamMemberId:int}")]
        public async Task<IActionResult> GetTeamMemberById(int teamMemberId)
        {
            try
            {
                string requestJson = JsonConvert.SerializeObject(teamMemberId);
                logger.LogInformation($"Request: GetteamMemberById:{requestJson}");

                var result = await teamMemberRepository.GetTeamMemberById(teamMemberId);
                if (result == null)
                    return NotFound();

                var response = mapper.Map<TeamMemberEdit>(result);

                string responseJson = JsonConvert.SerializeObject(response);
                logger.LogInformation($"Response: GetTeamMemberById: {responseJson}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GetDesignationById");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> InsertTeamMember(TeamMemberAdd teamMemberAdd)
        {
            try
            {
                string requestJson = JsonConvert.SerializeObject(teamMemberAdd);
                logger.LogInformation($"Request: Insert TeamMember:{requestJson}");
                
                TeamMember teamMember = mapper.Map<TeamMember>(teamMemberAdd);
                var result = await teamMemberRepository.InsertTeamMember(teamMember);

                var response = mapper.Map<TeamMemberEdit>(result);

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: Inserted Designation:{responseJson}");

                return CreatedAtAction(nameof(GetTeamMemberById), new { teamMemberId = teamMember.TeamMemberId }, response);
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
        public async Task<IActionResult> UpdateTeamMember(TeamMemberEdit teamMemberEdit)
        {
            try
            {
                var data = await teamMemberRepository.GetTeamMemberById(teamMemberEdit.TeamMemberId);
                if (data == null)
                {
                    return NotFound();
                }
                string requestJson = JsonConvert.SerializeObject(teamMemberEdit);

                logger.LogInformation($"Request: Update TeamMember: {requestJson}");

                data = mapper.Map<TeamMember>(teamMemberEdit);
                data.ModifiedOn = DateTime.Now;
                data.ModifiedBy = "Admin";
                data.ModifiedFrom = "::1";
                var result = await teamMemberRepository.UpdateTeamMember(data);
                var response = mapper.Map<TeamMemberEdit>(result);

                string responseJson = JsonConvert.SerializeObject(response);

                logger.LogInformation($"Response: Updated TeamMember:{responseJson}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in UpdateTeamMemeber");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete]
        [Route("{teamMemberId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int teamMemberId)
        {
            try
            {
                var data = await teamMemberRepository.GetTeamMemberById(teamMemberId);
                if (data == null)
                {
                    return NotFound();
                }
                string requestJson = JsonConvert.SerializeObject(data);

                logger.LogInformation($"Request: DeleteTeamMember: {requestJson}");

                data.IsDaleted = true;
                data.DeletedOn = DateTime.Now;
                data.DeletedBy = "Admin";
                data.DeletedFrom = "::1";
                var result = await teamMemberRepository.DeleteTeamMember(data);

                string responseJson = JsonConvert.SerializeObject(result);

                logger.LogInformation($"Response: Deleted TeamMember: {responseJson}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in DeleteTeamMember");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
