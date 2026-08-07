using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Repositories
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllByUserIdAsync(int userId);

        Task<Notification?> GetByIdAsync(int notificationId);

        Task AddAsync(Notification notification);

        Task UpdateAsync(Notification notification);

        Task DeleteAsync(int notificationId);

        Task SaveChangesAsync();
    }
}