using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRecruitment.API.Enums;
using SmartRecruitment.API.Models.DTOs;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost]
        [Authorize(Roles = "JobSeeker")]
        public async Task<IActionResult> Apply([FromBody] ApplyJobRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                // In production, extract JobSeekerProfileId from JWT Claims
                int jobSeekerProfileId = 1;

                var result = await _applicationService.ApplyAsync(jobSeekerProfileId, dto);
                return CreatedAtAction(nameof(GetById), new { id = result.ApplicationId }, result);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpGet("jobseeker/{jobSeekerProfileId}")]
        [Authorize(Roles = "JobSeeker,Admin")]
        public async Task<IActionResult> GetByJobSeeker(int jobSeekerProfileId)
        {
            var list = await _applicationService.GetJobSeekerApplicationsAsync(jobSeekerProfileId);
            return Ok(list);
        }

        [HttpGet("vacancy/{vacancyId}")]
        [Authorize(Roles = "Employer,Admin")]
        public async Task<IActionResult> GetByVacancy(int vacancyId)
        {
            var list = await _applicationService.GetVacancyApplicationsAsync(vacancyId);
            return Ok(list);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Employer,Admin")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] ApplicationStatus newStatus)
        {
            bool updated = await _applicationService.UpdateApplicationStatusAsync(id, newStatus);
            if (!updated) return NotFound(new { message = "Application not found." });

            return NoContent();
        }
    }
}