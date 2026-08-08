using SmartRecruitment.API.Models.DTOs.JobSeeker;

namespace SmartRecruitment.API.Services.Interfaces
{
    public interface IJobSeekerService
    {// Profile
        Task<JobSeekerProfileResponseDto?>
            GetCurrentProfileAsync(
                int userId);

        Task<JobSeekerProfileResponseDto>
            CreateOrUpdateProfileAsync(
                int userId,
                UpdateJobSeekerProfileRequestDto request);

        // Skills
        Task<IReadOnlyList<JobSeekerSkillResponseDto>>
            GetSkillsAsync(
                int userId);

        Task<JobSeekerSkillResponseDto>
            AddSkillAsync(
                int userId,
                AddJobSeekerSkillRequestDto request);

  

        Task RemoveSkillAsync(
            int userId,
            int skillId);

        // Education
        Task<IReadOnlyList<EducationResponseDto>>
            GetEducationAsync(
                int userId);

        Task<EducationResponseDto>
            CreateEducationAsync(
                int userId,
                CreateEducationRequestDto request);

        Task<EducationResponseDto>
            UpdateEducationAsync(
                int userId,
                int educationId,
                UpdateEducationRequestDto request);

        Task DeleteEducationAsync(
            int userId,
            int educationId);

        // Experience
        Task<IReadOnlyList<ExperienceResponseDto>>
            GetExperiencesAsync(
                int userId);

        Task<ExperienceResponseDto>
            CreateExperienceAsync(
                int userId,
                CreateExperienceRequestDto request);

        Task<ExperienceResponseDto>
            UpdateExperienceAsync(
                int userId,
                int experienceId,
                UpdateExperienceRequestDto request);

        Task DeleteExperienceAsync(
            int userId,
            int experienceId);
    }
}
