using SmartRecruitment.API.Models.DTOs;

namespace SmartRecruitment.API.Services.Interfaces
{
    public interface IApplicationService
    {
        Task<bool> ApplyJobAsync(ApplyJobRequestDto request);

        Task<bool> HasAlreadyAppliedAsync(int jobSeekerId, int vacancyId);
    }
}