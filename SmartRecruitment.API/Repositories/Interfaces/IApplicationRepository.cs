using System.Collections.Generic;
using System.Threading.Tasks;
using SmartRecruitment.API.Enums;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Repositories.Interfaces
{
    public interface IApplicationRepository
    {
        Task<Application> CreateAsync(Application application);
        Task<bool> ExistsAsync(int jobSeekerProfileId, int vacancyId);
        Task<Application?> GetByIdAsync(int applicationId);
        Task<IEnumerable<Application>> GetByVacancyIdAsync(int vacancyId);
        Task<IEnumerable<Application>> GetByJobSeekerProfileIdAsync(int jobSeekerProfileId);
        Task<bool> UpdateStatusAsync(int applicationId, ApplicationStatus status);
    }
}