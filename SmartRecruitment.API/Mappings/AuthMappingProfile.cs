using AutoMapper;
using SmartRecruitment.API.Models.DTOs.Auth;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Mappings
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<User, AuthResponseDto>()
                .ForMember(
                    destination => destination.Token,
                    option => option.Ignore())
                .ForMember(
                    destination => destination.ExpiresAt,
                    option => option.Ignore());
        }
    }
}
