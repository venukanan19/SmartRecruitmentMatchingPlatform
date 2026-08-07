using SmartRecruitment.API.Models.DTOs.Employer;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories.Interfaces;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository _employerRepository;

        public EmployerService(
            IEmployerRepository employerRepository)
        {
            _employerRepository = employerRepository;
        }

        public async Task<EmployerProfileResponseDto?> GetProfileAsync(
            int userId)
        {
            var employerProfile =
                await _employerRepository.GetByUserIdAsync(userId);

            if (employerProfile == null)
            {
                return null;
            }

            return MapToResponseDto(employerProfile);
        }

        public async Task<EmployerProfileResponseDto> CreateProfileAsync(
            int userId,
            CreateEmployerProfileRequestDto request)
        {
            var existingProfile =
                await _employerRepository.GetByUserIdAsync(userId);

            if (existingProfile != null)
            {
                throw new InvalidOperationException(
                    "Employer profile already exists.");
            }

            var employerProfile = new EmployerProfile
            {
                UserId = userId,
                CompanyName = request.CompanyName.Trim(),
                CompanyDescription =
                    request.CompanyDescription.Trim(),
                Location = request.Location.Trim(),
                ContactNumber = request.ContactNumber.Trim(),
                Website = string.IsNullOrWhiteSpace(request.Website)
                    ? null
                    : request.Website.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            await _employerRepository.AddAsync(employerProfile);

            bool saved =
                await _employerRepository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "Employer profile could not be created.");
            }

            return MapToResponseDto(employerProfile);
        }

        public async Task<EmployerProfileResponseDto?> UpdateProfileAsync(
            int userId,
            UpdateEmployerProfileRequestDto request)
        {
            var employerProfile =
                await _employerRepository.GetByUserIdAsync(userId);

            if (employerProfile == null)
            {
                return null;
            }

            employerProfile.CompanyName =
                request.CompanyName.Trim();

            employerProfile.CompanyDescription =
                request.CompanyDescription.Trim();

            employerProfile.Location =
                request.Location.Trim();

            employerProfile.ContactNumber =
                request.ContactNumber.Trim();

            employerProfile.Website =
                string.IsNullOrWhiteSpace(request.Website)
                    ? null
                    : request.Website.Trim();

            employerProfile.UpdatedAt = DateTime.UtcNow;

            _employerRepository.Update(employerProfile);

            bool saved =
                await _employerRepository.SaveChangesAsync();

            if (!saved)
            {
                throw new InvalidOperationException(
                    "Employer profile could not be updated.");
            }

            return MapToResponseDto(employerProfile);
        }

        private static EmployerProfileResponseDto MapToResponseDto(
            EmployerProfile employerProfile)
        {
            return new EmployerProfileResponseDto
            {
                EmployerProfileId =
                    employerProfile.EmployerProfileId,
                CompanyName =
                    employerProfile.CompanyName,
                CompanyDescription =
                    employerProfile.CompanyDescription,
                Location =
                    employerProfile.Location,
                ContactNumber =
                    employerProfile.ContactNumber,
                Website =
                    employerProfile.Website,
                CreatedAt =
                    employerProfile.CreatedAt,
                UpdatedAt =
                    employerProfile.UpdatedAt
            };
        }
    }
}