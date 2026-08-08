using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Repositories
{
    public interface IJobSeekerRepository
    {
        // =====================================================
        // Profile
        // =====================================================

        Task<JobSeekerProfile?> GetByUserIdAsync(
            int userId);

        Task<JobSeekerProfile?> GetCompleteProfileByUserIdAsync(
            int userId);

        Task AddProfileAsync(
            JobSeekerProfile profile);

        // =====================================================
        // Skills
        // =====================================================

        Task<bool> SkillExistsAsync(
            int skillId);

        Task<bool> HasSkillAsync(
            int jobSeekerProfileId,
            int skillId);

        Task AddSkillAsync(
            JobSeekerSkill skill);

        Task<JobSeekerSkill?> GetSkillRelationAsync(
            int userId,
            int skillId);

        void RemoveSkill(
            JobSeekerSkill skill);

        // =====================================================
        // Education
        // =====================================================

        Task<IReadOnlyList<Education>>
            GetEducationAsync(
                int userId);

        Task<Education?>
            GetEducationByIdAsync(
                int userId,
                int educationId);

        Task AddEducationAsync(
            Education education);

        void UpdateEducation(
            Education education);

        void RemoveEducation(
            Education education);

        // =====================================================
        // Experience
        // =====================================================

        Task<IReadOnlyList<Experience>>
            GetExperiencesAsync(
                int userId);

        Task<Experience?>
            GetExperienceByIdAsync(
                int userId,
                int experienceId);

        Task AddExperienceAsync(
            Experience experience);

        void UpdateExperience(
            Experience experience);

        void RemoveExperience(
            Experience experience);

        // =====================================================
        // Save
        // =====================================================

        Task<bool> SaveChangesAsync();
    }
}