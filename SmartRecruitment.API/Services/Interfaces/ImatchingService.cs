using SmartRecruitment.API.Models.DTOs;

namespace SmartRecruitment.API.Services.Interfaces
{
    public interface IMatchingService
    {
        Task<MatchResultDto> GetMatchScoreAsync(int vacancyId, int jobSeekerId);

        Task<List<RankedCandidateDto>> GetRankedCandidatesAsync(int vacancyId);
    }
}