using System.ComponentModel.DataAnnotations;

namespace SmartRecruitment.API.Models.DTOs.Notification
{
    public class MarkNotificationReadRequestDto
    {
        [Required]
        public int NotificationId { get; set; }
    }
}
