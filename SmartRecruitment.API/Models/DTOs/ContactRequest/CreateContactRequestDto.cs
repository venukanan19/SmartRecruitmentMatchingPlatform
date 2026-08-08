using System.ComponentModel.DataAnnotations;

namespace SmartRecruitment.API.Models.DTOs.ContactRequest
{
    public class CreateContactRequestDto
    {
        [Required]
        public int JobSeekerProfileId { get; set; }

        [MaxLength(500)]
        public string? Message { get; set; }
    }
}