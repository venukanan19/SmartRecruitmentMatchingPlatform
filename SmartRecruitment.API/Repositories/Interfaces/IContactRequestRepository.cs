using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Repositories.Interfaces
{
    public interface IContactRequestRepository
    {
        Task<IEnumerable<ContactRequest>> GetAllAsync();

        Task<ContactRequest?> GetByIdAsync(int contactRequestId);

        Task<IEnumerable<ContactRequest>> GetByEmployerIdAsync(int employerProfileId);

        Task<IEnumerable<ContactRequest>> GetByJobSeekerIdAsync(int jobSeekerProfileId);

        Task AddAsync(ContactRequest contactRequest);

        Task UpdateAsync(ContactRequest contactRequest);

        Task DeleteAsync(ContactRequest contactRequest);

        Task SaveChangesAsync();
    }
}

