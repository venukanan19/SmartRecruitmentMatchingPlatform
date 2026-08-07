using System.ComponentModel.DataAnnotations;

namespace SmartRecruitment.API.Models.DTOs
{
    public class ApplyJobRequestDto
    {
        [Required]
        public int VacancyId { get; set; }

        [MaxLength(1000)]
        public string? CoverLetter { get; set; }
    }
}