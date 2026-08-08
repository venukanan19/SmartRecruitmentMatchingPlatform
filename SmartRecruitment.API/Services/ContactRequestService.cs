using SmartRecruitment.API.Enums;
using SmartRecruitment.API.Models.DTOs.ContactRequest;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories.Interfaces;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
    public class ContactRequestService : IContactRequestService
    {
        private readonly IContactRequestRepository _repository;

        public ContactRequestService(
            IContactRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContactRequestResponseDto?> GetByIdAsync(
            int contactRequestId)
        {
            var contactRequest =
                await _repository.GetByIdAsync(contactRequestId);

            if (contactRequest == null)
                return null;

            return MapToResponseDto(contactRequest);
        }

        public async Task<IEnumerable<ContactRequestResponseDto>>
            GetByEmployerIdAsync(int employerProfileId)
        {
            var requests =
                await _repository.GetByEmployerIdAsync(employerProfileId);

            return requests.Select(MapToResponseDto);
        }

        public async Task<IEnumerable<ContactRequestResponseDto>>
            GetByJobSeekerIdAsync(int jobSeekerProfileId)
        {
            var requests =
                await _repository.GetByJobSeekerIdAsync(jobSeekerProfileId);

            return requests.Select(MapToResponseDto);
        }

        public async Task<ContactRequestResponseDto> CreateAsync(
            int employerProfileId,
            CreateContactRequestDto request)
        {
            var contactRequest = new ContactRequest
            {
                EmployerProfileId = employerProfileId,
                JobSeekerProfileId = request.JobSeekerProfileId,
                Status = ContactRequestStatus.pending,
                RequestedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(contactRequest);
            await _repository.SaveChangesAsync();

            return MapToResponseDto(contactRequest);
        }

        public async Task<bool> UpdateStatusAsync(
            int contactRequestId,
            UpdateContactRequestStatusDto request)
        {
            var contactRequest =
                await _repository.GetByIdAsync(contactRequestId);

            if (contactRequest == null)
                return false;

            contactRequest.Status = request.Status;
            contactRequest.RespondedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(contactRequest);
            await _repository.SaveChangesAsync();

            return true;
        }

        private static ContactRequestResponseDto MapToResponseDto(
            ContactRequest contactRequest)
        {
            return new ContactRequestResponseDto
            {
                ContactRequestId = contactRequest.ContactRequestId,
                EmployerProfileId = contactRequest.EmployerProfileId,
                JobSeekerProfileId = contactRequest.JobSeekerProfileId,
                Status = contactRequest.Status,
                RequestedAt = contactRequest.RequestedAt,
                RespondedAt = contactRequest.RespondedAt
            };
        }
    }
}