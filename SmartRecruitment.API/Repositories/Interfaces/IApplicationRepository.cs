using SmartRecruitment.API.Models.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace SmartRecruitment.API.Repositories.Interfaces
{
    public interface IApplicationRepository
    {
        Task<bool> ExistsAsync(int jobSeekerId, int vacancyId);

        Task AddAsync(Application application);

        Task SaveChangesAsync();
    }
} 