using SmartRecruitment.API.Models.Entities; 

namespace SmartRecruitment.API.Repositories.Interfaces
{
    public interface IVacancyRepository
    {
        Task<Vacancy?> GetByIdAsync(int vacancyId);
    }
}