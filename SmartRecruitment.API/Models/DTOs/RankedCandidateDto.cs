namespace SmartRecruitment.API.Models.DTOs
{
    public class RankedCandidateDto
    {
        internal double TotalScore;
        internal int JobSeekerProfileId;

        public int JobSeekerId { get; set; }

        public string CandidateName { get; set; } = string.Empty;

        public double MatchScore { get; set; }

        public int Rank { get; set; }
    }
}