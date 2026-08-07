using SmartRecruitment.API.Data;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories.Interfaces;

namespace SmartRecruitment.API.Repositories
{
    public class CvMetadataRepository
       : ICvMetadataRepository
    {
        private readonly ApplicationDbContext _context;

        public CvMetadataRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<CvMetadata?>
        //    GetByUserIdAsync(
        //        int userId)
        //{
        //    return await _context.CvMetadata
        //        .Include(
        //            metadata =>
        //                metadata.JobSeekerProfile)
        //        .FirstOrDefaultAsync(
        //            metadata =>
        //                metadata.JobSeekerProfile.UserId ==
        //                    userId);
        //}

        public async Task AddAsync(
            CvMetadata metadata)
        {
            await _context.CvMetadata
                .AddAsync(metadata);
        }

        public void Update(
            CvMetadata metadata)
        {
            _context.CvMetadata
                .Update(metadata);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
