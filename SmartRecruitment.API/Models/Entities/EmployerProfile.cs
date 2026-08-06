namespace SmartRecruitment.API.Models.Entities
{
    public class EmployerProfile
    {
        public int EmployerProfileId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        public string CompanyName { get; set; } = string.Empty;

        public string CompanyDescription { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string ContactNumber { get; set; } = string.Empty;

        public string? Website { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public ICollection<Vacancy> Vacancies { get; set; }
            = new List<Vacancy>();
    }
}