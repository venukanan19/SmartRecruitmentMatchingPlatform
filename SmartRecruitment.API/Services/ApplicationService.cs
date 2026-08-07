using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartRecruitment.API.Enums;
using SmartRecruitment.API.Models.DTOs;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories.Interfaces;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<Application> ApplyAsync(int jobSeekerProfileId, ApplyJobRequestDto request)
        {
            bool alreadyApplied = await _applicationRepository.ExistsAsync(jobSeekerProfileId, request.VacancyId);
            if (alreadyApplied)
            {
                throw new InvalidOperationException("You have already submitted an application for this vacancy.");
            }

            var application = new Application
            {
                JobSeekerProfileId = jobSeekerProfileId,
                VacancyId = request.VacancyId,
                CoverLetter = request.CoverLetter,
                Status = ApplicationStatus.Applied,
                AppliedDate = DateTime.UtcNow
            };

            return await _applicationRepository.CreateAsync(application);
        }

        public async Task<IEnumerable<Application>> GetJobSeekerApplicationsAsync(int jobSeekerProfileId)
        {
            return await _applicationRepository.GetByJobSeekerProfileIdAsync(jobSeekerProfileId);
        }

        public async Task<IEnumerable<Application>> GetVacancyApplicationsAsync(int vacancyId)
        {
            return await _applicationRepository.GetByVacancyIdAsync(vacancyId);
        }

        public async Task<bool> UpdateApplicationStatusAsync(int applicationId, ApplicationStatus status)
        {
            return await _applicationRepository.UpdateStatusAsync(applicationId, status);
        }
    }
}