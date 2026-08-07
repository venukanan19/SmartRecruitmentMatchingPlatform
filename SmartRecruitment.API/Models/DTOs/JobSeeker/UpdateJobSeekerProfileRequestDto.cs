namespace SmartRecruitment.API.Models.DTOs.JobSeeker
{
    public class UpdateJobSeekerProfileRequestDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;
    }
}
