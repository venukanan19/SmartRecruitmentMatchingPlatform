using Microsoft.EntityFrameworkCore;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        //public DbSet<ContactRequest> ContactRequests { get; set; }
    }
}