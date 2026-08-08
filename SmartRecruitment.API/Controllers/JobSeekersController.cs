using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRecruitment.API.Constants;
using SmartRecruitment.API.Models.DTOs.JobSeeker;
using SmartRecruitment.API.Services.Interfaces;
using System.Security.Claims;

namespace SmartRecruitment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "JobSeeker")]
    public class JobSeekersController : ControllerBase
    {
        private readonly IJobSeekerService _jobSeekerService;
        private readonly ICvStorageService _cvStorageService;

        public JobSeekersController(
            IJobSeekerService jobSeekerService,
            ICvStorageService cvStorageService)
        {
            _jobSeekerService = jobSeekerService;
            _cvStorageService = cvStorageService;
        }

        // =========================================================
        // Profile
        // =========================================================

        [HttpGet("me")]
        public async Task<ActionResult<JobSeekerProfileResponseDto>>
            GetCurrentProfile()
        {
            var userId = GetAuthenticatedUserId();

            var profile =
                await _jobSeekerService.GetCurrentProfileAsync(userId);

            if (profile == null)
            {
                return NotFound(new
                {
                    message = "Job Seeker profile was not found."
                });
            }

            return Ok(profile);
        }

        [HttpPut("me")]
        public async Task<ActionResult<JobSeekerProfileResponseDto>>
            UpdateCurrentProfile(
                [FromBody] UpdateJobSeekerProfileRequestDto request)
        {
            var profile =
                await _jobSeekerService.CreateOrUpdateProfileAsync(
                    GetAuthenticatedUserId(),
                    request);

            return Ok(profile);
        }

        // =========================================================
        // Skills
        // =========================================================

        [HttpGet("skills")]
        public async Task<ActionResult<IReadOnlyList<JobSeekerSkillResponseDto>>>
            GetSkills()
        {
            var skills =
                await _jobSeekerService.GetSkillsAsync(
                    GetAuthenticatedUserId());

            return Ok(skills);
        }

        [HttpPost("skills")]
        public async Task<ActionResult<JobSeekerSkillResponseDto>>
            AddSkill(
                [FromBody] AddJobSeekerSkillRequestDto request)
        {
            var result =
                await _jobSeekerService.AddSkillAsync(
                    GetAuthenticatedUserId(),
                    request);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpDelete("skills/{skillId:int}")]
        public async Task<IActionResult>
            RemoveSkill(int skillId)
        {
            await _jobSeekerService.RemoveSkillAsync(
                GetAuthenticatedUserId(),
                skillId);

            return NoContent();
        }

        // =========================================================
        // Education
        // =========================================================

        [HttpGet("education")]
        public async Task<ActionResult<IReadOnlyList<EducationResponseDto>>>
            GetEducation()
        {
            var result =
                await _jobSeekerService.GetEducationAsync(
                    GetAuthenticatedUserId());

            return Ok(result);
        }

        [HttpPost("education")]
        public async Task<ActionResult<EducationResponseDto>>
            CreateEducation(
                [FromBody] CreateEducationRequestDto request)
        {
            var result =
                await _jobSeekerService.CreateEducationAsync(
                    GetAuthenticatedUserId(),
                    request);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("education/{educationId:int}")]
        public async Task<ActionResult<EducationResponseDto>>
            UpdateEducation(
                int educationId,
                [FromBody] UpdateEducationRequestDto request)
        {
            var result =
                await _jobSeekerService.UpdateEducationAsync(
                    GetAuthenticatedUserId(),
                    educationId,
                    request);

            return Ok(result);
        }

        [HttpDelete("education/{educationId:int}")]
        public async Task<IActionResult>
            DeleteEducation(int educationId)
        {
            await _jobSeekerService.DeleteEducationAsync(
                GetAuthenticatedUserId(),
                educationId);

            return NoContent();
        }

        // =========================================================
        // Experience
        // =========================================================

        [HttpGet("experiences")]
        public async Task<ActionResult<IReadOnlyList<ExperienceResponseDto>>>
            GetExperiences()
        {
            var result =
                await _jobSeekerService.GetExperiencesAsync(
                    GetAuthenticatedUserId());

            return Ok(result);
        }

        [HttpPost("experiences")]
        public async Task<ActionResult<ExperienceResponseDto>>
            CreateExperience(
                [FromBody] CreateExperienceRequestDto request)
        {
            var result =
                await _jobSeekerService.CreateExperienceAsync(
                    GetAuthenticatedUserId(),
                    request);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPut("experiences/{experienceId:int}")]
        public async Task<ActionResult<ExperienceResponseDto>>
            UpdateExperience(
                int experienceId,
                [FromBody] UpdateExperienceRequestDto request)
        {
            var result =
                await _jobSeekerService.UpdateExperienceAsync(
                    GetAuthenticatedUserId(),
                    experienceId,
                    request);

            return Ok(result);
        }

        [HttpDelete("experiences/{experienceId:int}")]
        public async Task<IActionResult>
            DeleteExperience(int experienceId)
        {
            await _jobSeekerService.DeleteExperienceAsync(
                GetAuthenticatedUserId(),
                experienceId);

            return NoContent();
        }

        // =========================================================
        // CV
        // =========================================================

        [HttpPost("cv")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(CvStorageConstants.MaximumFileSizeBytes)]
        public async Task<ActionResult<CvUploadResponseDto>>
            UploadCv([FromForm] IFormFile file)
        {
            var result =
                await _cvStorageService.UploadOrReplaceAsync(
                    GetAuthenticatedUserId(),
                    file);

            return Ok(result);
        }

        [HttpGet("cv/metadata")]
        public async Task<ActionResult<CvMetadataResponseDto>>
            GetCvMetadata()
        {
            var metadata =
                await _cvStorageService.GetMetadataAsync(
                    GetAuthenticatedUserId());

            if (metadata == null)
            {
                return NotFound(new
                {
                    message = "CV metadata was not found."
                });
            }

            return Ok(metadata);
        }

        [HttpGet("cv/content")]
        public async Task<IActionResult>
            GetCvContent()
        {
            var result =
                await _cvStorageService.GetContentAsync(
                    GetAuthenticatedUserId());

            if (result == null)
            {
                return NotFound(new
                {
                    message = "CV file was not found."
                });
            }

            return File(
                result.Stream,
                result.ContentType,
                result.DownloadFileName);
        }

        private int GetAuthenticatedUserId()
        {
            var value =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(value, out var userId))
            {
                throw new UnauthorizedAccessException(
                    "The authenticated UserId claim is missing or invalid.");
            }

            return userId;
        }
    }
}