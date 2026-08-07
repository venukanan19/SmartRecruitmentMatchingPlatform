using System.ComponentModel.DataAnnotations;
using SmartRecruitment.API.Enums;

namespace SmartRecruitment.API.Models.DTOs.ContactRequest
{
    public class UpdateContactRequestStatusDto
    {
        [Required]
        public ContactRequestStatus Status { get; set; }
    }
}
