using Microsoft.AspNetCore.Mvc;
using SmartRecruitment.API.Models.DTOs.Notification;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<NotificationResponseDto>>> GetUserNotifications(int userId)
        {
            var notifications = await _notificationService.GetUserNotificationsAsync(userId);

            return Ok(notifications);
        }

        [HttpGet("{notificationId}")]
        public async Task<ActionResult<NotificationResponseDto>> GetNotificationById(int notificationId)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(notificationId);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }

        [HttpPut("{notificationId}/read")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            var success = await _notificationService.MarkAsReadAsync(notificationId);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{notificationId}")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            var success = await _notificationService.DeleteNotificationAsync(notificationId);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
