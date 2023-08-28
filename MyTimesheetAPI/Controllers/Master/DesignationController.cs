using AutoMapper;
using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MyTimesheetAPI.Controllers.Master
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationRepository designationRepository;
        private readonly IMapper mapper;

        public DesignationController(IDesignationRepository designationRepository, IMapper mapper)
        {
            this.designationRepository = designationRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result  = await designationRepository.GetAllDesignationAsync();
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{designationId:int}")]
        public async Task<IActionResult> GetById(int designationId)
        {
            var result = await designationRepository.GetDesignationById(designationId);
            if(result == null) 
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(DesignationAddDTO designationAddDTO)
        {
            Designation designation = mapper.Map<Designation>(designationAddDTO);
            var result = await designationRepository.Add(designation);
            return CreatedAtAction(nameof(GetById), new { designationId= designation.DesignationId }, result);
            
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(DesignationEditDTO designationEditDTO)
        {
            var data = await designationRepository.GetDesignationById(designationEditDTO.DesignationId);
            if(data == null)
            {
                return NotFound();
            }
            data = mapper.Map<Designation>(designationEditDTO);
            //To-Do - next to replace with standard library to determine IP address from the request and User Name from the token
            data.ModifiedOn = DateTime.Now;
            data.ModifiedBy = "Admin";
            data.ModifiedFrom = "::1";
            var result = await designationRepository.Edit(data);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{designationId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int designationId)
        {
            var data = await designationRepository.GetDesignationById(designationId);
            if(data == null)
            {
                return NotFound();
            }
            data.IsDaleted = true;
            data.DeletedOn = DateTime.Now;
            data.DeletedBy = "Admin";
            data.DeletedFrom = "::1";
            var result = await designationRepository.Delete(data);
            return Ok(result);
        }
    }
}
