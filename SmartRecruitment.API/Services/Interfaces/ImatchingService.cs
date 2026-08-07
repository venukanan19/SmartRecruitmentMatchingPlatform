using System.Collections.Generic;
using System.Threading.Tasks;
using SmartRecruitment.API.Models.DTOs;

namespace SmartRecruitment.API.Services.Interfaces
{
    public interface IMatchingService
    {
        Task<MatchResultDto> CalculateMatchAsync(int jobSeekerProfileId, int vacancyId);
        Task<IEnumerable<RankedCandidateDto>> GetRankedCandidatesAsync(int vacancyId);
    }
}