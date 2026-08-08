using AutoMapper;
using SmartRecruitment.API.Enums;
using SmartRecruitment.API.Helpers;
using SmartRecruitment.API.Models.DTOs.Auth;
using SmartRecruitment.API.Models.Entities;
using SmartRecruitment.API.Repositories.Interfaces;
using SmartRecruitment.API.Services.Interfaces;

namespace SmartRecruitment.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;

        public AuthService(
            IUserRepository userRepository,
            JwtTokenGenerator jwtTokenGenerator,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> RegisterJobSeekerAsync(
            RegisterJobSeekerRequestDto request,
            CancellationToken cancellationToken = default)
        {
            return await RegisterUserAsync(
                request.FullName,
                request.Email,
                request.Password,
                UserRole.JobSeeker,
                cancellationToken);
        }

        public async Task<AuthResponseDto> RegisterEmployerAsync(
            RegisterEmployerRequestDto request,
            CancellationToken cancellationToken = default)
        {
            return await RegisterUserAsync(
                request.FullName,
                request.Email,
                request.Password,
                UserRole.Employer,
                cancellationToken);
        }

        public async Task<AuthResponseDto> LoginAsync(
            LoginRequestDto request,
            CancellationToken cancellationToken = default)
        {
            string normalizedEmail =
                NormalizeEmail(request.Email);

            User? user = await _userRepository.GetByEmailAsync(
                normalizedEmail,
                cancellationToken);

            if (user is null)
            {
                throw new UnauthorizedAccessException(
                    "Invalid email or password.");
            }

            bool passwordIsValid =
                PasswordHashHelper.VerifyPassword(
                    request.Password,
                    user.PasswordHash);

            if (!passwordIsValid)
            {
                throw new UnauthorizedAccessException(
                    "Invalid email or password.");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException(
                    "This account is inactive.");
            }

            return CreateAuthResponse(user);
        }

        private async Task<AuthResponseDto> RegisterUserAsync(
            string fullName,
            string email,
            string password,
            UserRole role,
            CancellationToken cancellationToken)
        {
            string normalizedEmail = NormalizeEmail(email);

            bool emailExists =
                await _userRepository.EmailExistsAsync(
                    normalizedEmail,
                    cancellationToken);

            if (emailExists)
            {
                throw new InvalidOperationException(
                    "An account with this email address already exists.");
            }

            User user = new()
            {
                FullName = fullName.Trim(),
                Email = normalizedEmail,
                PasswordHash =
                    PasswordHashHelper.HashPassword(password),
                Role = role.ToString(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(
                user,
                cancellationToken);

            await _userRepository.SaveChangesAsync(
                cancellationToken);

            return CreateAuthResponse(user);
        }

        private AuthResponseDto CreateAuthResponse(User user)
        {
            AuthResponseDto response =
                _mapper.Map<AuthResponseDto>(user);

            (string token, DateTime expiresAt) =
                _jwtTokenGenerator.GenerateToken(user);

            response.Token = token;
            response.ExpiresAt = expiresAt;

            return response;
        }

        private static string NormalizeEmail(string email)
        {
            return email.Trim().ToLowerInvariant();
        }
    }
}
