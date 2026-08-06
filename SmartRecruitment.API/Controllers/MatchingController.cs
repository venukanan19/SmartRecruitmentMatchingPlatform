using Microsoft.AspNetCore.Http;
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

        [HttpGet("score")]
        public async Task<IActionResult> GetMatchScore(
            int vacancyId,
            int jobSeekerId)
        {
            var result =
                await _matchingService.GetMatchScoreAsync(vacancyId, jobSeekerId);

            return Ok(result);
        }

        [HttpGet("ranking/{vacancyId}")]
        public async Task<IActionResult> GetRanking(int vacancyId)
        {
            var result =
                await _matchingService.GetRankedCandidatesAsync(vacancyId);

            return Ok(result);
        }
    }
}