using SmartRecruitment.API.Models.DTOs.Notification;

namespace SmartRecruitment.API.Services
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationResponseDto>> GetUserNotificationsAsync(int userId);

        Task<NotificationResponseDto?> GetNotificationByIdAsync(int notificationId);

        Task<bool> MarkAsReadAsync(int notificationId);

        Task<bool> DeleteNotificationAsync(int notificationId);
    }
}
