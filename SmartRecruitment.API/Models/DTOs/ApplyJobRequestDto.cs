using System.ComponentModel.DataAnnotations;

namespace SmartRecruitment.API.Models.DTOs
{
    public class ApplyJobRequestDto
    {
        
        public int VacancyId { get; set; }

        
        public string? CoverLetter { get; set; }
    }
}