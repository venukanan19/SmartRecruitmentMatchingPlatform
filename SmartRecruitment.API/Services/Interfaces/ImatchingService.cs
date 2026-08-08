using SmartRecruitment.API.Models.DTOs;

namespace SmartRecruitment.API.Services.Interfaces
{
    public interface IMatchingService
    {
        Task<MatchResultDto> CalculateMatchAsync(
            int userId,
            int vacancyId);

        Task<IEnumerable<RankedCandidateDto>>
            GetRankedCandidatesAsync(
                int vacancyId);
    }
}