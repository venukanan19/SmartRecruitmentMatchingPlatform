using SmartRecruitment.API.Models.DTOs.Notification;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<IEnumerable<NotificationResponseDto>> GetUserNotificationsAsync(int userId)
        {
            var notifications = await _notificationRepository.GetAllByUserIdAsync(userId);

            return notifications.Select(n => new NotificationResponseDto
            {
                NotificationId = n.NotificationId,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            });
        }

        public async Task<NotificationResponseDto?> GetNotificationByIdAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);

            if (notification == null)
                return null;

            return new NotificationResponseDto
            {
                NotificationId = notification.NotificationId,
                Title = notification.Title,
                Message = notification.Message,
                IsRead = notification.IsRead,
                CreatedAt = notification.CreatedAt
            };
        }

        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);

            if (notification == null)
                return false;

            notification.IsRead = true;

            await _notificationRepository.UpdateAsync(notification);
            await _notificationRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteNotificationAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetByIdAsync(notificationId);

            if (notification == null)
                return false;

            await _notificationRepository.DeleteAsync(notificationId);
            await _notificationRepository.SaveChangesAsync();

            return true;
        }
    }
}
