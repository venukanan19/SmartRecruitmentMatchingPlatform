using SmartRecruitment.API.Models.DTOs.Admin;

namespace SmartRecruitment.API.Services.Interfaces
{
    public interface IAdminService
    {
        Task<IReadOnlyList<UserAccountResponseDto>> GetUsersAsync(
            CancellationToken cancellationToken = default);

        Task<UserAccountResponseDto> GetUserByIdAsync(
            int userId,
            CancellationToken cancellationToken = default);

        Task<UserAccountResponseDto> UpdateUserStatusAsync(
            int userId,
            UpdateUserAccountStatusRequestDto request,
            int currentAdminUserId,
            CancellationToken cancellationToken = default);

        Task<AdminDashboardResponseDto> GetDashboardAsync(
            CancellationToken cancellationToken = default);
    }
}
