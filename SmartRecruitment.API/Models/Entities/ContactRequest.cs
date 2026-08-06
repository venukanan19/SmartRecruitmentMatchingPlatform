//using SmartRecruitment.API.Enums;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace SmartRecruitment.API.Models.Entities
//{
//    public class ContactRequest
//    {
//        [Key]
//        public int ContactRequestId { get; set; }

//        [Required]
//        public int EmployerProfileId { get; set; }

//        [ForeignKey(nameof(EmployerProfileId))]
//        public EmployerProfile EmployerProfile { get; set; } = null!;

//        [Required]
//        public int JobSeekerProfileId { get; set; }

//        [ForeignKey(nameof(JobSeekerProfileId))]
//        public JobSeekerProfile JobSeekerProfile { get; set; } = null!;

//        [Required]
//        public ContactRequestStatus Status { get; set; } = ContactRequestStatus.pending;

//        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

//        public DateTime? RespondedAt { get; set; }
//    }
//}

