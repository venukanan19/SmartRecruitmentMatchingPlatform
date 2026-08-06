using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Data.Configurations
{
    public class CvMetadataConfiguration
    {
        public int CvMetadataId { get; set; }

        public int JobSeekerProfileId { get; set; }

        public string OriginalFileName { get; set; }
            = string.Empty;
 
        public long FileSize { get; set; }

        public DateTime UploadedAt { get; set; }
            = DateTime.UtcNow;

        public JobSeekerProfile JobSeekerProfile
        { get; set; } = null!;
    }
}
