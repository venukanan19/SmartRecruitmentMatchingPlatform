using SmartRecruitment.API.Enums;

namespace SmartRecruitment.API.Models.DTOs.ContactRequest
{
    public class ContactRequestResponseDto
    {
        public int ContactRequestId { get; set; }

        public int EmployerProfileId { get; set; }

        public int JobSeekerProfileId { get; set; }

        public ContactRequestStatus Status { get; set; }

        public DateTime RequestedAt { get; set; }

        public DateTime? RespondedAt { get; set; }
    }
}
