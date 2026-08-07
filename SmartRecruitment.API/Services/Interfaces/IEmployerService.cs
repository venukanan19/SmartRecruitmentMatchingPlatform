using SmartRecruitment.API.Models.DTOs.Employer;

namespace SmartRecruitment.API.Services.Interfaces
{
    public interface IEmployerService
    {
        Task<EmployerProfileResponseDto?> GetProfileAsync(int userId);

        Task<EmployerProfileResponseDto> CreateProfileAsync(
            int userId,
            CreateEmployerProfileRequestDto request);

        Task<EmployerProfileResponseDto?> UpdateProfileAsync(
            int userId,
            UpdateEmployerProfileRequestDto request);
    }
}