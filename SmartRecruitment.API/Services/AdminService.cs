using AutoMapper;
using SmartRecruitment.API.Enums;
using SmartRecruitment.API.Models.DTOs.Admin;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories.Interfaces;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
        public class AdminService : IAdminService
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public AdminService(
                IUserRepository userRepository,
                IMapper mapper)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }

            public async Task<IReadOnlyList<UserAccountResponseDto>>
                GetUsersAsync(
                    CancellationToken cancellationToken = default)
            {
                IReadOnlyList<User> users =
                    await _userRepository.GetAllAsync(
                        cancellationToken);

                return _mapper.Map<
                    IReadOnlyList<UserAccountResponseDto>>(users);
            }

            public async Task<UserAccountResponseDto> GetUserByIdAsync(
                int userId,
                CancellationToken cancellationToken = default)
            {
                User user = await GetRequiredUserAsync(
                    userId,
                    cancellationToken);

                return _mapper.Map<UserAccountResponseDto>(user);
            }

            public async Task<UserAccountResponseDto>
                UpdateUserStatusAsync(
                    int userId,
                    UpdateUserAccountStatusRequestDto request,
                    int currentAdminUserId,
                    CancellationToken cancellationToken = default)
            {
                User user = await GetRequiredUserAsync(
                    userId,
                    cancellationToken);

                if (user.UserId == currentAdminUserId &&
                    !request.IsActive)
                {
                    throw new InvalidOperationException(
                        "You cannot deactivate your own administrator account.");
                }

                user.IsActive = request.IsActive;

                await _userRepository.UpdateAsync(
                    user,
                    cancellationToken);

                await _userRepository.SaveChangesAsync(
                    cancellationToken);

                return _mapper.Map<UserAccountResponseDto>(user);
            }

            public async Task<AdminDashboardResponseDto>
                GetDashboardAsync(
                    CancellationToken cancellationToken = default)
            {
                int totalUsers =
                    await _userRepository.CountAllAsync(
                        cancellationToken);

                int activeUsers =
                    await _userRepository.CountActiveAsync(
                        cancellationToken);

                int totalJobSeekers =
                    await _userRepository.CountByRoleAsync(
                        UserRole.JobSeeker.ToString(),
                        cancellationToken);

                int totalEmployers =
                    await _userRepository.CountByRoleAsync(
                        UserRole.Employer.ToString(),
                        cancellationToken);

                int totalAdministrators =
                    await _userRepository.CountByRoleAsync(
                        UserRole.Admin.ToString(),
                        cancellationToken);

                return new AdminDashboardResponseDto
                {
                    TotalUsers = totalUsers,
                    ActiveUsers = activeUsers,
                    InactiveUsers = totalUsers - activeUsers,
                    TotalJobSeekers = totalJobSeekers,
                    TotalEmployers = totalEmployers,
                    TotalAdministrators = totalAdministrators
                };
            }

            private async Task<User> GetRequiredUserAsync(
                int userId,
                CancellationToken cancellationToken)
            {
                User? user = await _userRepository.GetByIdAsync(
                    userId,
                    cancellationToken);

                if (user is null)
                {
                    throw new KeyNotFoundException(
                        $"User with ID {userId} was not found.");
                }

                return user;
            }
        }   
}
