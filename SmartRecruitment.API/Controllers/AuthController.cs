using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartRecruitment.API.Models.DTOs.Auth;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register/jobseeker")]
        [ProducesResponseType(
            typeof(AuthResponseDto),
            StatusCodes.Status201Created)]
        [ProducesResponseType(
            StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
            StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RegisterJobSeeker(
            [FromBody] RegisterJobSeekerRequestDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                AuthResponseDto response =
                    await _authService.RegisterJobSeekerAsync(
                        request,
                        cancellationToken);

                return StatusCode(
                    StatusCodes.Status201Created,
                    response);
            }
            catch (InvalidOperationException exception)
            {
                return Conflict(new
                {
                    message = exception.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("register/employer")]
        [ProducesResponseType(
            typeof(AuthResponseDto),
            StatusCodes.Status201Created)]
        [ProducesResponseType(
            StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
            StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RegisterEmployer(
            [FromBody] RegisterEmployerRequestDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                AuthResponseDto response =
                    await _authService.RegisterEmployerAsync(
                        request,
                        cancellationToken);

                return StatusCode(
                    StatusCodes.Status201Created,
                    response);
            }
            catch (InvalidOperationException exception)
            {
                return Conflict(new
                {
                    message = exception.Message
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(
            typeof(AuthResponseDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(
            StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
            StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto request,
            CancellationToken cancellationToken)
        {
            try
            {
                AuthResponseDto response =
                    await _authService.LoginAsync(
                        request,
                        cancellationToken);

                return Ok(response);
            }
            catch (UnauthorizedAccessException exception)
            {
                return Unauthorized(new
                {
                    message = exception.Message
                });
            }
        }
    }
}
