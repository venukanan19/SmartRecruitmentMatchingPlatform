namespace SmartRecruitment.API.Models.DTOs.JobSeeker
{
    public class CvMetadataResponseDto
    {
        public int CvMetadataId { get; set; }

        public string OriginalFileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public long FileSize { get; set; }

        public DateTime UploadedAt { get; set; }
    }
}
