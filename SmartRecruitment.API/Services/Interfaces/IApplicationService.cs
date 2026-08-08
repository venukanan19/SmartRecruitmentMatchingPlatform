using System.Collections.Generic;
using System.Threading.Tasks;
using SmartRecruitment.API.Enums;
using SmartRecruitment.API.Models.DTOs;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<Application> ApplyAsync(int jobSeekerProfileId, ApplyJobRequestDto request);
        Task<IEnumerable<Application>> GetJobSeekerApplicationsAsync(int jobSeekerProfileId);
        Task<IEnumerable<Application>> GetVacancyApplicationsAsync(int vacancyId);
        Task<bool> UpdateApplicationStatusAsync(int applicationId, ApplicationStatus status);
    }
}