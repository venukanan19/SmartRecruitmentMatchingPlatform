using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Data;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Repositories
{
    public class ContactRequestRepository : IContactRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactRequest>> GetAllAsync()
        {
            return await _context.ContactRequests
                .Include(cr => cr.EmployerProfile)
                .Include(cr => cr.JobSeekerProfile)
                .OrderByDescending(cr => cr.RequestedAt)
                .ToListAsync();
        }

        public async Task<ContactRequest?> GetByIdAsync(int contactRequestId)
        {
            return await _context.ContactRequests
                .Include(cr => cr.EmployerProfile)
                .Include(cr => cr.JobSeekerProfile)
                .FirstOrDefaultAsync(cr => cr.ContactRequestId == contactRequestId);
        }

        public async Task<IEnumerable<ContactRequest>> GetByEmployerIdAsync(
            int employerProfileId)
        {
            return await _context.ContactRequests
                .Include(cr => cr.EmployerProfile)
                .Include(cr => cr.JobSeekerProfile)
                .Where(cr => cr.EmployerProfileId == employerProfileId)
                .OrderByDescending(cr => cr.RequestedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<ContactRequest>> GetByJobSeekerIdAsync(
            int jobSeekerProfileId)
        {
            return await _context.ContactRequests
                .Include(cr => cr.EmployerProfile)
                .Include(cr => cr.JobSeekerProfile)
                .Where(cr => cr.JobSeekerProfileId == jobSeekerProfileId)
                .OrderByDescending(cr => cr.RequestedAt)
                .ToListAsync();
        }

        public async Task AddAsync(ContactRequest contactRequest)
        {
            await _context.ContactRequests.AddAsync(contactRequest);
        }

        public async Task UpdateAsync(ContactRequest contactRequest)
        {
            _context.ContactRequests.Update(contactRequest);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(ContactRequest contactRequest)
        {
            _context.ContactRequests.Remove(contactRequest);
            await Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
