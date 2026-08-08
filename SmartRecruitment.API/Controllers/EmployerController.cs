using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRecruitment.API.Models.DTOs.Employer;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Controllers
{
    [ApiController]
    [Route("api/employer")]
    [Authorize(Roles = "Employer")]
    public class EmployerController : ControllerBase
    {
        private readonly IEmployerService _employerService;

        public EmployerController(
            IEmployerService employerService)
        {
            _employerService = employerService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            if (!TryGetUserId(out int userId))
            {
                return Unauthorized();
            }

            var profile =
                await _employerService.GetProfileAsync(userId);

            if (profile == null)
            {
                return NotFound(new
                {
                    message = "Employer profile not found."
                });
            }

            return Ok(profile);
        }

        [HttpPost("profile")]
        public async Task<IActionResult> CreateProfile(
            [FromBody] CreateEmployerProfileRequestDto request)
        {
            if (!TryGetUserId(out int userId))
            {
                return Unauthorized();
            }

            try
            {
                var profile =
                    await _employerService.CreateProfileAsync(
                        userId,
                        request);

                return CreatedAtAction(
                    nameof(GetProfile),
                    profile);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(
            [FromBody] UpdateEmployerProfileRequestDto request)
        {
            if (!TryGetUserId(out int userId))
            {
                return Unauthorized();
            }

            var profile =
                await _employerService.UpdateProfileAsync(
                    userId,
                    request);

            if (profile == null)
            {
                return NotFound(new
                {
                    message = "Employer profile not found."
                });
            }

            return Ok(profile);
        }

        private bool TryGetUserId(out int userId)
        {
            string? userIdValue =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(userIdValue, out userId);
        }
    }
}