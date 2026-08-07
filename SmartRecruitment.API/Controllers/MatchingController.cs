using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchingController : ControllerBase
    {
        private readonly IMatchingService _matchingService;

        public MatchingController(IMatchingService matchingService)
        {
            _matchingService = matchingService;
        }

        [HttpGet("calculate")]
        [Authorize]
        public async Task<IActionResult> CalculateMatch([FromQuery] int jobSeekerProfileId, [FromQuery] int vacancyId)
        {
            var result = await _matchingService.CalculateMatchAsync(jobSeekerProfileId, vacancyId);
            return Ok(result);
        }

        [HttpGet("ranked-candidates/{vacancyId}")]
        [Authorize(Roles = "Employer,Admin")]
        public async Task<IActionResult> GetRankedCandidates(int vacancyId)
        {
            var candidates = await _matchingService.GetRankedCandidatesAsync(vacancyId);
            return Ok(candidates);
        }
    }
}