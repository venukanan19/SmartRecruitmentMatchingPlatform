using SmartRecruitment.API.Models.DTOs.ContactRequest;

namespace SmartRecruitment.API.Services
{
    public interface IContactRequestService
    {
        Task<ContactRequestResponseDto?> GetByIdAsync(int contactRequestId);

        Task<IEnumerable<ContactRequestResponseDto>> GetByEmployerIdAsync(
            int employerProfileId);

        Task<IEnumerable<ContactRequestResponseDto>> GetByJobSeekerIdAsync(
            int jobSeekerProfileId);

        Task<ContactRequestResponseDto> CreateAsync(
            CreateContactRequestDto request);

        Task<bool> UpdateStatusAsync(
            int contactRequestId,
            UpdateContactRequestStatusDto request);
    }
}