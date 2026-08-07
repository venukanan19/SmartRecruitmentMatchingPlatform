using SmartRecruitment.API.Models.DTOs;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
    public class ApplicationService : IApplicationService
    {
        public async Task<bool> ApplyJobAsync(ApplyJobRequestDto request)
        {
            // TODO:
            // Duplicate Check

            // TODO:
            // Save Application

            throw new NotImplementedException();
        }

        public async Task<bool> HasAlreadyAppliedAsync(
            int jobSeekerId,
            int vacancyId)
        {
            // TODO:

            throw new NotImplementedException();
        }
    }
}