using SmartRecruitment.API.Models.DTOs.Vacancy;

namespace SmartRecruitment.API.Services.Interfaces
{
    public interface IVacancyService
    {
        Task<VacancyResponseDto?> GetByIdAsync(int vacancyId);

        Task<IReadOnlyList<VacancyResponseDto>>
            GetEmployerVacanciesAsync(int userId);

        Task<IReadOnlyList<VacancyResponseDto>> SearchAsync(
            VacancySearchRequestDto request);

        Task<VacancyResponseDto> CreateAsync(
            int userId,
            CreateVacancyRequestDto request);

        Task<VacancyResponseDto?> UpdateAsync(
            int userId,
            int vacancyId,
            UpdateVacancyRequestDto request);
    }
}