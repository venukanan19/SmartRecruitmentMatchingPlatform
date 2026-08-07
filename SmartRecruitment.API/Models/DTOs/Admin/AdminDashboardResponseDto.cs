namespace SmartRecruitment.API.Models.DTOs.Admin
{
    public class AdminDashboardResponseDto
    {
        public int TotalUsers { get; set; }

        public int ActiveUsers { get; set; }

        public int InactiveUsers { get; set; }

        public int TotalJobSeekers { get; set; }

        public int TotalEmployers { get; set; }

        public int TotalAdministrators { get; set; }
    }
}
