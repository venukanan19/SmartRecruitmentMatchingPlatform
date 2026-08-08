using SmartRecruitment.API.Models.DTOs;

namespace SmartRecruitment.API.Services
{
    public class RankingService
    {
        public List<RankedCandidateDto> GetRankedCandidates(
            IEnumerable<RankedCandidateDto> candidates)
        {
            return candidates
                .OrderByDescending(x => x.TotalScore)
                .Select((candidate, index) =>
                {
                    candidate.Rank = index + 1;
                    return candidate;
                })
                .ToList();
        }
    }
}