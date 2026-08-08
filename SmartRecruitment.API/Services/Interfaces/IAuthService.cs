using SmartRecruitment.API.Models.DTOs.Auth;

namespace SmartRecruitment.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterJobSeekerAsync(
            RegisterJobSeekerRequestDto request,
            CancellationToken cancellationToken = default);

        Task<AuthResponseDto> RegisterEmployerAsync(
            RegisterEmployerRequestDto request,
            CancellationToken cancellationToken = default);

        Task<AuthResponseDto> LoginAsync(
            LoginRequestDto request,
            CancellationToken cancellationToken = default);
    }
}
