//using SmartRecruitment.API.Data;
//using SmartRecruitment.API.Models.Entities;
//using SmartRecruitment.API.Repositories.Interfaces;

//namespace SmartRecruitment.API.Repositories
//{
//    public class JobSeekerRepository
//        : IJobSeekerRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public JobSeekerRepository(
//            ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // =========================================================
//        // Profile
//        // =========================================================

//        public async Task<JobSeekerProfile?> GetByUserIdAsync(
//            int userId)
//        {
//            return await _context.JobSeekerProfiles
//                .FirstOrDefaultAsync(
//                    profile =>
//                        profile.UserId == userId);
//        }

//        public async Task<JobSeekerProfile?>
//            GetCompleteProfileByUserIdAsync(
//                int userId)
//        {
//            return await _context.JobSeekerProfiles
//                .AsNoTracking()
//                .Include(profile => profile.User)
//                .Include(profile => profile.JobSeekerSkills)
//                    .ThenInclude(
//                        profileSkill =>
//                            profileSkill.Skill)
//                .Include(profile => profile.Educations)
//                .Include(profile => profile.Experiences)
//                .Include(profile => profile.CvMetadata)
//                .FirstOrDefaultAsync(
//                    profile =>
//                        profile.UserId == userId);
//        }

//        public async Task AddProfileAsync(
//            JobSeekerProfile profile)
//        {
//            await _context.JobSeekerProfiles
//                .AddAsync(profile);
//        }

//        // =========================================================
//        // Shared Skill
//        // =========================================================

//        public async Task<bool> SkillExistsAsync(
//            int skillId)
//        {
//            return await _context.Skills
//                .AnyAsync(
//                    skill =>
//                        skill.SkillId == skillId);
//        }

//        // =========================================================
//        // Job Seeker Skills
//        // =========================================================

//        public async Task<bool> HasSkillAsync(
//            int profileId,
//            int skillId)
//        {
//            return await _context.JobSeekerSkills
//                .AnyAsync(
//                    profileSkill =>
//                        profileSkill.JobSeekerProfileId ==
//                            profileId &&
//                        profileSkill.SkillId ==
//                            skillId);
//        }

//        public async Task AddSkillAsync(
//            JobSeekerSkill jobSeekerSkill)
//        {
//            await _context.JobSeekerSkills
//                .AddAsync(jobSeekerSkill);
//        }

//        public async Task<JobSeekerSkill?>
//            GetSkillRelationAsync(
//                int userId,
//                int skillId)
//        {
//            return await _context.JobSeekerSkills
//                .Include(
//                    profileSkill =>
//                        profileSkill.Skill)
//                .Include(
//                    profileSkill =>
//                        profileSkill.JobSeekerProfile)
//                .FirstOrDefaultAsync(
//                    profileSkill =>
//                        profileSkill.SkillId == skillId &&
//                        profileSkill.JobSeekerProfile.UserId ==
//                            userId);
//        }

//        public void UpdateSkill(
//            JobSeekerSkill jobSeekerSkill)
//        {
//            _context.JobSeekerSkills
//                .Update(jobSeekerSkill);
//        }

//        public void RemoveSkill(
//            JobSeekerSkill jobSeekerSkill)
//        {
//            _context.JobSeekerSkills
//                .Remove(jobSeekerSkill);
//        }

//        // =========================================================
//        // Education
//        // =========================================================

//        public async Task<IReadOnlyList<Education>>
//            GetEducationAsync(
//                int userId)
//        {
//            return await _context.Educations
//                .AsNoTracking()
//                .Where(
//                    education =>
//                        education.JobSeekerProfile.UserId ==
//                            userId)
//                .OrderByDescending(
//                    education =>
//                        education.StartDate)
//                .ToListAsync();
//        }

//        public async Task<Education?>
//            GetEducationByIdAsync(
//                int userId,
//                int educationId)
//        {
//            return await _context.Educations
//                .Include(
//                    education =>
//                        education.JobSeekerProfile)
//                .FirstOrDefaultAsync(
//                    education =>
//                        education.EducationId ==
//                            educationId &&
//                        education.JobSeekerProfile.UserId ==
//                            userId);
//        }

//        public async Task AddEducationAsync(
//            Education education)
//        {
//            await _context.Educations
//                .AddAsync(education);
//        }

//        public void UpdateEducation(
//            Education education)
//        {
//            _context.Educations
//                .Update(education);
//        }

//        public void RemoveEducation(
//            Education education)
//        {
//            _context.Educations
//                .Remove(education);
//        }

//        // =========================================================
//        // Experience
//        // =========================================================

//        public async Task<IReadOnlyList<Experience>>
//            GetExperiencesAsync(
//                int userId)
//        {
//            return await _context.Experiences
//                .AsNoTracking()
//                .Where(
//                    experience =>
//                        experience.JobSeekerProfile.UserId ==
//                            userId)
//                .OrderByDescending(
//                    experience =>
//                        experience.StartDate)
//                .ToListAsync();
//        }

//        public async Task<Experience?>
//            GetExperienceByIdAsync(
//                int userId,
//                int experienceId)
//        {
//            return await _context.Experiences
//                .Include(
//                    experience =>
//                        experience.JobSeekerProfile)
//                .FirstOrDefaultAsync(
//                    experience =>
//                        experience.ExperienceId ==
//                            experienceId &&
//                        experience.JobSeekerProfile.UserId ==
//                            userId);
//        }

//        public async Task AddExperienceAsync(
//            Experience experience)
//        {
//            await _context.Experiences
//                .AddAsync(experience);
//        }

//        public void UpdateExperience(
//            Experience experience)
//        {
//            _context.Experiences
//                .Update(experience);
//        }

//        public void RemoveExperience(
//            Experience experience)
//        {
//            _context.Experiences
//                .Remove(experience);
//        }

//        // =========================================================
//        // Save
//        // =========================================================

//        public async Task<bool> SaveChangesAsync()
//        {
//            return await _context.SaveChangesAsync() > 0;
//        }
//    }
//}
