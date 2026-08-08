using AutoMapper;
using SmartRecruitment.API.Models.DTOs.JobSeeker;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
    public class JobSeekerService
    : IJobSeekerService
    {
        private readonly IJobSeekerRepository _repository;
        private readonly IMapper _mapper;

        public JobSeekerService(
            IJobSeekerRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // =========================================================
        // Profile
        // =========================================================

        public async Task<JobSeekerProfileResponseDto?>
            GetCurrentProfileAsync(
                int userId)
        {
            var profile =
                await _repository
                    .GetCompleteProfileByUserIdAsync(
                        userId);

            return profile is null
                ? null
                : _mapper.Map<
                    JobSeekerProfileResponseDto>(
                        profile);
        }

        public async Task<JobSeekerProfileResponseDto>
            CreateOrUpdateProfileAsync(
                int userId,
                UpdateJobSeekerProfileRequestDto request)
        {
            var profile =
                await _repository
                    .GetByUserIdAsync(userId);

            var currentTime = DateTime.UtcNow;

            if (profile is null)
            {
                profile =
                    _mapper.Map<JobSeekerProfile>(
                        request);

                profile.UserId = userId;
                profile.CreatedAt = currentTime;
                profile.UpdatedAt = currentTime;

                await _repository
                    .AddProfileAsync(profile);
            }
            else
            {
                _mapper.Map(
                    request,
                    profile);

                profile.UpdatedAt =
                    currentTime;
            }

            var saved =
                await _repository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "The Job Seeker profile could not be saved.");
            }

            var completeProfile =
                await _repository
                    .GetCompleteProfileByUserIdAsync(
                        userId);

            if (completeProfile is null)
            {
                throw new InvalidOperationException(
                    "The saved Job Seeker profile could not be retrieved.");
            }

            return _mapper.Map<
                JobSeekerProfileResponseDto>(
                    completeProfile);
        }

        // =========================================================
        // Skills
        // =========================================================

        public async Task<
            IReadOnlyList<JobSeekerSkillResponseDto>>
            GetSkillsAsync(
                int userId)
        {
            var profile =
                await _repository
                    .GetCompleteProfileByUserIdAsync(
                        userId);

            if (profile is null)
            {
                throw new KeyNotFoundException(
                    "Job Seeker profile was not found.");
            }

            return _mapper.Map<
                IReadOnlyList<JobSeekerSkillResponseDto>>(
                    profile.JobSeekerSkills);
        }

        public async Task<JobSeekerSkillResponseDto>
            AddSkillAsync(
                int userId,
                AddJobSeekerSkillRequestDto request)
        {
            var profile =
                await _repository
                    .GetByUserIdAsync(userId);

            if (profile is null)
            {
                throw new KeyNotFoundException(
                    "Create the Job Seeker profile before adding skills.");
            }

            var skillExists =
                await _repository
                    .SkillExistsAsync(
                        request.SkillId);

            if (!skillExists)
            {
                throw new KeyNotFoundException(
                    "The selected skill does not exist.");
            }

            var duplicateExists =
                await _repository
                    .HasSkillAsync(
                        profile.JobSeekerProfileId,
                        request.SkillId);

            if (duplicateExists)
            {
                throw new InvalidOperationException(
                    "The skill is already assigned to this profile.");
            }

            var profileSkill =
                new JobSeekerSkill
                {
                    JobSeekerProfileId =
                        profile.JobSeekerProfileId,

                    SkillId =
                        request.SkillId,


                };

            await _repository
                .AddSkillAsync(profileSkill);

            var saved =
                await _repository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "The skill could not be added.");
            }

            var savedSkill =
                await _repository
                    .GetSkillRelationAsync(
                        userId,
                        request.SkillId);

            if (savedSkill is null)
            {
                throw new InvalidOperationException(
                    "The saved skill could not be retrieved.");
            }

            return _mapper.Map<
                JobSeekerSkillResponseDto>(
                    savedSkill);
        }



        public async Task RemoveSkillAsync(
            int userId,
            int skillId)
        {
            var profileSkill =
                await _repository
                    .GetSkillRelationAsync(
                        userId,
                        skillId);

            if (profileSkill is null)
            {
                throw new KeyNotFoundException(
                    "The skill was not found for this Job Seeker.");
            }

            _repository.RemoveSkill(
                profileSkill);

            var saved =
                await _repository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "The skill could not be removed.");
            }
        }

        // =========================================================
        // Education
        // =========================================================

        public async Task<
            IReadOnlyList<EducationResponseDto>>
            GetEducationAsync(
                int userId)
        {
            var education =
                await _repository
                    .GetEducationAsync(userId);

            return _mapper.Map<
                IReadOnlyList<EducationResponseDto>>(
                    education);
        }

        public async Task<EducationResponseDto>
            CreateEducationAsync(
                int userId,
                CreateEducationRequestDto request)
        {
            var profile =
                await _repository
                    .GetByUserIdAsync(userId);

            if (profile is null)
            {
                throw new KeyNotFoundException(
                    "Create the Job Seeker profile before adding education.");
            }

            var education =
                _mapper.Map<Education>(
                    request);

            education.JobSeekerProfileId =
                profile.JobSeekerProfileId;

            education.CreatedAt =
                DateTime.UtcNow;

            await _repository
                .AddEducationAsync(
                    education);

            var saved =
                await _repository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "The education record could not be created.");
            }

            return _mapper.Map<
                EducationResponseDto>(
                    education);
        }

        public async Task<EducationResponseDto>
            UpdateEducationAsync(
                int userId,
                int educationId,
                UpdateEducationRequestDto request)
        {
            var education =
                await _repository
                    .GetEducationByIdAsync(
                        userId,
                        educationId);

            if (education is null)
            {
                throw new KeyNotFoundException(
                    "The education record was not found.");
            }

            _mapper.Map(
                request,
                education);

            _repository.UpdateEducation(
                education);

            var saved =
                await _repository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "The education record could not be updated.");
            }

            return _mapper.Map<
                EducationResponseDto>(
                    education);
        }

        public async Task DeleteEducationAsync(
            int userId,
            int educationId)
        {
            var education =
                await _repository
                    .GetEducationByIdAsync(
                        userId,
                        educationId);

            if (education is null)
            {
                throw new KeyNotFoundException(
                    "The education record was not found.");
            }

            _repository.RemoveEducation(
                education);

            var saved =
                await _repository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "The education record could not be deleted.");
            }
        }

        // =========================================================
        // Experience
        // =========================================================

        public async Task<
            IReadOnlyList<ExperienceResponseDto>>
            GetExperiencesAsync(
                int userId)
        {
            var experiences =
                await _repository
                    .GetExperiencesAsync(
                        userId);

            return _mapper.Map<
                IReadOnlyList<ExperienceResponseDto>>(
                    experiences);
        }

        public async Task<ExperienceResponseDto>
            CreateExperienceAsync(
                int userId,
                CreateExperienceRequestDto request)
        {
            var profile =
                await _repository
                    .GetByUserIdAsync(userId);

            if (profile is null)
            {
                throw new KeyNotFoundException(
                    "Create the Job Seeker profile before adding experience.");
            }

            var experience =
                _mapper.Map<Experience>(
                    request);

            experience.JobSeekerProfileId =
                profile.JobSeekerProfileId;

            experience.CreatedAt =
                DateTime.UtcNow;

            await _repository
                .AddExperienceAsync(
                    experience);

            var saved =
                await _repository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "The experience record could not be created.");
            }

            return _mapper.Map<
                ExperienceResponseDto>(
                    experience);
        }

        public async Task<ExperienceResponseDto>
            UpdateExperienceAsync(
                int userId,
                int experienceId,
                UpdateExperienceRequestDto request)
        {
            var experience =
                await _repository
                    .GetExperienceByIdAsync(
                        userId,
                        experienceId);

            if (experience is null)
            {
                throw new KeyNotFoundException(
                    "The experience record was not found.");
            }

            _mapper.Map(
                request,
                experience);

            _repository.UpdateExperience(
                experience);

            var saved =
                await _repository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "The experience record could not be updated.");
            }

            return _mapper.Map<
                ExperienceResponseDto>(
                    experience);
        }

        public async Task DeleteExperienceAsync(
            int userId,
            int experienceId)
        {
            var experience =
                await _repository
                    .GetExperienceByIdAsync(
                        userId,
                        experienceId);

            if (experience is null)
            {
                throw new KeyNotFoundException(
                    "The experience record was not found.");
            }

            _repository.RemoveExperience(
                experience);

            var saved =
                await _repository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "The experience record could not be deleted.");
            }
        }
    }
}
