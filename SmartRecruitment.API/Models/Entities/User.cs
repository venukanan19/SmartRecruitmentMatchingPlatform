namespace SmartRecruitment.API.Models.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Member 3 integration point.
        public EmployerProfile? EmployerProfile { get; set; }

        // Member 2 integration point.
        public JobSeekerProfile? JobSeekerProfile { get; set; }
    }
}