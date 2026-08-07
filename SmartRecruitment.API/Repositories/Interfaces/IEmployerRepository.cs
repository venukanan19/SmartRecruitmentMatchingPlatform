using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Repositories.Interfaces
{
    public interface IEmployerRepository
    {
        Task<EmployerProfile?> GetByUserIdAsync(int userId);

        Task<EmployerProfile?> GetByIdAsync(int employerProfileId);

        Task AddAsync(EmployerProfile employerProfile);

        void Update(EmployerProfile employerProfile);

        Task<bool> SaveChangesAsync();
    }
}