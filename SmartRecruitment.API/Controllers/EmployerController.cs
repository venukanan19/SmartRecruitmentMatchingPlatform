using System.Security.Claims;
using FluentValidation;
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

        private readonly IValidator<CreateEmployerProfileRequestDto>
            _createProfileValidator;

        private readonly IValidator<UpdateEmployerProfileRequestDto>
            _updateProfileValidator;

        public EmployerController(
            IEmployerService employerService,
            IValidator<CreateEmployerProfileRequestDto>
                createProfileValidator,
            IValidator<UpdateEmployerProfileRequestDto>
                updateProfileValidator)
        {
            _employerService = employerService;
            _createProfileValidator = createProfileValidator;
            _updateProfileValidator = updateProfileValidator;
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
            var validationResult =
                await _createProfileValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    errors = validationResult.Errors
                        .Select(error => error.ErrorMessage)
                        .ToList()
                });
            }

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
            var validationResult =
                await _updateProfileValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(new
                {
                    errors = validationResult.Errors
                        .Select(error => error.ErrorMessage)
                        .ToList()
                });
            }

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
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            return int.TryParse(
                userIdValue,
                out userId);
        }
    }
}