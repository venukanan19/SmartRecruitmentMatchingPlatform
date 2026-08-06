using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartRecruitment.API.Models.DTOs;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplyJob(ApplyJobRequestDto request)
        {
            var result = await _applicationService.ApplyJobAsync(request);

            if (!result)
                return Conflict("You have already applied for this vacancy.");

            return Ok("Application submitted successfully.");
        }
    }
}