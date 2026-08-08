using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Repositories.Interfaces
{
    public interface ICvMetadataRepository
    {
        Task<CvMetadata?> GetByUserIdAsync(
           int userId);

        Task AddAsync(
            CvMetadata metadata);

        void Update(
            CvMetadata metadata);

        Task<bool> SaveChangesAsync();
    }
}
