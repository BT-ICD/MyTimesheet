using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyTimesheetAPI.Controllers.Master
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationRepository _designationRepository;

        public DesignationController(IDesignationRepository designationRepository)
        {
            _designationRepository = designationRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDesignations()
        {
            var result  = await _designationRepository.GetAllDesignationAsync();
            return Ok(result);
        }
    }
}
