using SmartRecruitment.API.Models.DTOs.Vacancy;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories.Interfaces;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IEmployerRepository _employerRepository;

        public VacancyService(
            IVacancyRepository vacancyRepository,
            IEmployerRepository employerRepository)
        {
            _vacancyRepository = vacancyRepository;
            _employerRepository = employerRepository;
        }

        public async Task<VacancyResponseDto?> GetByIdAsync(
            int vacancyId)
        {
            var vacancy =
                await _vacancyRepository.GetByIdWithDetailsAsync(
                    vacancyId);

            if (vacancy == null)
            {
                return null;
            }

            return MapToResponseDto(vacancy);
        }

        public async Task<IReadOnlyList<VacancyResponseDto>>
            GetEmployerVacanciesAsync(int userId)
        {
            var employer =
                await _employerRepository.GetByUserIdAsync(userId);

            if (employer == null)
            {
                return Array.Empty<VacancyResponseDto>();
            }

            var vacancies =
                await _vacancyRepository.GetByEmployerIdAsync(
                    employer.EmployerProfileId);

            return vacancies
                .Select(MapToResponseDto)
                .ToList();
        }

        public async Task<IReadOnlyList<VacancyResponseDto>> SearchAsync(
            VacancySearchRequestDto request)
        {
            var vacancies =
                await _vacancyRepository.SearchAsync(
                    request.SearchTerm,
                    request.Location,
                    request.MaxRequiredExperienceYears,
                    request.SkillId);

            return vacancies
                .Select(MapToResponseDto)
                .ToList();
        }

        public async Task<VacancyResponseDto> CreateAsync(
            int userId,
            CreateVacancyRequestDto request)
        {
            var employer =
                await _employerRepository.GetByUserIdAsync(userId);

            if (employer == null)
            {
                throw new InvalidOperationException(
                    "Employer profile does not exist.");
            }

            var skillIds = request.RequiredSkillIds
                .Distinct()
                .ToList();

            var skills =
                await _vacancyRepository.GetSkillsByIdsAsync(
                    skillIds);

            if (skills.Count != skillIds.Count)
            {
                throw new ArgumentException(
                    "One or more required skills are invalid.");
            }

            var vacancy = new Vacancy
            {
                EmployerProfileId =
                    employer.EmployerProfileId,

                Title = request.Title.Trim(),

                Description = request.Description.Trim(),

                Location = request.Location.Trim(),

                RequiredExperienceYears =
                    request.RequiredExperienceYears,

                EducationRequirement =
                    request.EducationRequirement.Trim(),

                CreatedAt = DateTime.UtcNow
            };

            foreach (var skill in skills)
            {
                vacancy.VacancySkills.Add(
                    new VacancySkill
                    {
                        SkillId = skill.SkillId
                    });
            }

            await _vacancyRepository.AddAsync(vacancy);

            bool saved =
                await _vacancyRepository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "Vacancy could not be created.");
            }

            return MapToResponseDto(vacancy);
        }

        public async Task<VacancyResponseDto?> UpdateAsync(
            int userId,
            int vacancyId,
            UpdateVacancyRequestDto request)
        {
            var employer =
                await _employerRepository.GetByUserIdAsync(userId);

            if (employer == null)
            {
                return null;
            }

            var vacancy =
                await _vacancyRepository.GetByIdWithDetailsAsync(
                    vacancyId);

            if (vacancy == null)
            {
                return null;
            }

            // Ownership check
            if (vacancy.EmployerProfileId !=
                employer.EmployerProfileId)
            {
                throw new UnauthorizedAccessException(
                    "You cannot update this vacancy.");
            }

            var skillIds = request.RequiredSkillIds
                .Distinct()
                .ToList();

            var skills =
                await _vacancyRepository.GetSkillsByIdsAsync(
                    skillIds);

            if (skills.Count != skillIds.Count)
            {
                throw new ArgumentException(
                    "One or more required skills are invalid.");
            }

            vacancy.Title = request.Title.Trim();

            vacancy.Description =
                request.Description.Trim();

            vacancy.Location =
                request.Location.Trim();

            vacancy.RequiredExperienceYears =
                request.RequiredExperienceYears;

            vacancy.EducationRequirement =
                request.EducationRequirement.Trim();

            vacancy.UpdatedAt = DateTime.UtcNow;

            // Replace existing required skills
            vacancy.VacancySkills.Clear();

            foreach (var skill in skills)
            {
                vacancy.VacancySkills.Add(
                    new VacancySkill
                    {
                        VacancyId = vacancy.VacancyId,
                        SkillId = skill.SkillId
                    });
            }

            _vacancyRepository.Update(vacancy);

            bool saved =
                await _vacancyRepository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "Vacancy could not be updated.");
            }

            return MapToResponseDto(vacancy);
        }

        private static VacancyResponseDto MapToResponseDto(
            Vacancy vacancy)
        {
            return new VacancyResponseDto
            {
                VacancyId = vacancy.VacancyId,

                EmployerProfileId =
                    vacancy.EmployerProfileId,

                CompanyName =
                    vacancy.EmployerProfile?.CompanyName
                    ?? string.Empty,

                Title = vacancy.Title,

                Description = vacancy.Description,

                Location = vacancy.Location,

                RequiredExperienceYears =
                    vacancy.RequiredExperienceYears,

                EducationRequirement =
                    vacancy.EducationRequirement,

                Status = vacancy.Status.ToString(),

                RequiredSkills = vacancy.VacancySkills
                    .Where(vacancySkill =>
                        vacancySkill.Skill != null)
                    .Select(vacancySkill =>
                        vacancySkill.Skill.Name)
                    .OrderBy(name => name)
                    .ToList(),

                CreatedAt = vacancy.CreatedAt,

                UpdatedAt = vacancy.UpdatedAt
            };
        }
    }
}