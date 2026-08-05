namespace SmartRecruitment.API.Models.Entities
{
    public class CvMetadata
    {
        public int CvMetadataId { get; set; }

        public int JobSeekerProfileId { get; set; }

        public string OriginalFileName { get; set; } = string.Empty; 

        public long FileSize { get; set; }

        public DateTime UploadedAt { get; set; }

        public JobSeekerProfile JobSeekerProfile { get; set; } = null!;
    }
}
