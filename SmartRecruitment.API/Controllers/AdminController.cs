using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartRecruitment.API.Enums;
using SmartRecruitment.API.Helpers;
using SmartRecruitment.API.Models.DTOs.Admin;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Admin))]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly CurrentUserAccessor _currentUserAccessor;

        public AdminController(
            IAdminService adminService,
            CurrentUserAccessor currentUserAccessor)
        {
            _adminService = adminService;
            _currentUserAccessor = currentUserAccessor;
        }

        [HttpGet("dashboard")]
        [ProducesResponseType(
            typeof(AdminDashboardResponseDto),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDashboard(
            CancellationToken cancellationToken)
        {
            AdminDashboardResponseDto response =
                await _adminService.GetDashboardAsync(
                    cancellationToken);

            return Ok(response);
        }

        [HttpGet("users")]
        [ProducesResponseType(
            typeof(IReadOnlyList<UserAccountResponseDto>),
            StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers(
            CancellationToken cancellationToken)
        {
            IReadOnlyList<UserAccountResponseDto> response =
                await _adminService.GetUsersAsync(
                    cancellationToken);

            return Ok(response);
        }

        [HttpGet("users/{userId:int}")]
        [ProducesResponseType(
            typeof(UserAccountResponseDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(
            StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(
            int userId,
            CancellationToken cancellationToken)
        {
            try
            {
                UserAccountResponseDto response =
                    await _adminService.GetUserByIdAsync(
                        userId,
                        cancellationToken);

                return Ok(response);
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(new
                {
                    message = exception.Message
                });
            }
        }

        [HttpPatch("users/{userId:int}/status")]
        [ProducesResponseType(
            typeof(UserAccountResponseDto),
            StatusCodes.Status200OK)]
        [ProducesResponseType(
            StatusCodes.Status400BadRequest)]
        [ProducesResponseType(
            StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUserStatus(
            int userId,
            [FromBody] UpdateUserAccountStatusRequestDto request,
            CancellationToken cancellationToken)
        {
            int? currentAdminUserId =
                _currentUserAccessor.GetCurrentUserId();

            if (currentAdminUserId is null)
            {
                return Unauthorized(new
                {
                    message = "Current administrator could not be identified."
                });
            }

            try
            {
                UserAccountResponseDto response =
                    await _adminService.UpdateUserStatusAsync(
                        userId,
                        request,
                        currentAdminUserId.Value,
                        cancellationToken);

                return Ok(response);
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(new
                {
                    message = exception.Message
                });
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(new
                {
                    message = exception.Message
                });
            }
        }
    }
}
