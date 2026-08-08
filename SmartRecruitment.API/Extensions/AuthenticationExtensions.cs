using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SmartRecruitment.API.Helpers;
using SmartRecruitment.API.Mappings;
using SmartRecruitment.API.Repositories;
using SmartRecruitment.API.Repositories.Interfaces;
using SmartRecruitment.API.Services;
using SmartRecruitment.API.Services.Interfaces;
using SmartRecruitment.API.Validators.Auth;
using System.Text;

namespace SmartRecruitment.API.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection
            AddAuthenticationModule(
                this IServiceCollection services,
                IConfiguration configuration)
        {
            string jwtKey = configuration["Jwt:Key"]
                ?? throw new InvalidOperationException(
                    "Jwt:Key is missing.");

            string jwtIssuer = configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException(
                    "Jwt:Issuer is missing.");

            string jwtAudience = configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException(
                    "Jwt:Audience is missing.");

            services.AddHttpContextAccessor();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAdminService, AdminService>();

            services.AddScoped<JwtTokenGenerator>();
            services.AddScoped<CurrentUserAccessor>();

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AuthMappingProfile>();
            });

            services.AddValidatorsFromAssemblyContaining<
                RegisterJobSeekerValidator>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,

                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(jwtKey)),

                            ValidateIssuer = true,
                            ValidIssuer = jwtIssuer,

                            ValidateAudience = true,
                            ValidAudience = jwtAudience,

                            ValidateLifetime = true,

                            ClockSkew = TimeSpan.Zero
                        };
                });

            services.AddAuthorization();

            return services;
        }
    }
}
