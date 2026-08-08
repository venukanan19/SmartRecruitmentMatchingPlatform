using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRecruitment.API.Models.DTOs.ContactRequest;
using SmartRecruitment.API.Repositories.Interfaces;
using SmartRecruitment.API.Services;
using System.Security.Claims;

namespace SmartRecruitment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactRequestsController : ControllerBase
    {
        private readonly IContactRequestService _contactRequestService;
        private readonly IEmployerRepository _employerRepository;

        public ContactRequestsController(
            IContactRequestService contactRequestService,
            IEmployerRepository employerRepository)
        {
            _contactRequestService = contactRequestService;
            _employerRepository = employerRepository;
        }

        [HttpGet("{contactRequestId:int}")]
        public async Task<IActionResult> GetById(int contactRequestId)
        {
            var result =
                await _contactRequestService.GetByIdAsync(
                    contactRequestId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("employer/{employerProfileId:int}")]
        public async Task<IActionResult> GetByEmployerId(
            int employerProfileId)
        {
            var result =
                await _contactRequestService
                    .GetByEmployerIdAsync(employerProfileId);

            return Ok(result);
        }

        [HttpGet("job-seeker/{jobSeekerProfileId:int}")]
        public async Task<IActionResult> GetByJobSeekerId(
            int jobSeekerProfileId)
        {
            var result =
                await _contactRequestService
                    .GetByJobSeekerIdAsync(jobSeekerProfileId);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateContactRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized();
            }

            var employerProfile =
                await _employerRepository.GetByUserIdAsync(userId);

            if (employerProfile == null)
            {
                return Forbid();
            }

            var result =
                await _contactRequestService.CreateAsync(
                    employerProfile.EmployerProfileId,
                    request);

            return Ok(result);
        }

        [HttpPut("{contactRequestId:int}/status")]
        public async Task<IActionResult> UpdateStatus(
            int contactRequestId,
            [FromBody] UpdateContactRequestStatusDto request)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            var updated =
                await _contactRequestService.UpdateStatusAsync(
                    contactRequestId,
                    request);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}