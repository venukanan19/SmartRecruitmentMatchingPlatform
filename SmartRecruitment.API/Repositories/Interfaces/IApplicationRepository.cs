using SmartRecruitment.API.Models.Entities;


namespace SmartRecruitment.API.Repositories.Interfaces
{
    public interface IApplicationRepository
    {
        Task<bool> ExistsAsync(int jobSeekerId, int vacancyId);

        Task AddAsync(Application application);

        Task SaveChangesAsync();
    }
}