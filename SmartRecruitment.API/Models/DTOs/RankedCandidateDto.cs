namespace SmartRecruitment.API.Models.DTOs
{
    public class RankedCandidateDto
    {
        public int Rank { get; set; }

        public int JobSeekerProfileId { get; set; }

        public string CandidateName { get; set; } = string.Empty;

        public double TotalScore { get; set; }
    }
}