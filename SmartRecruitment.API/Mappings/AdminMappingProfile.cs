using AutoMapper;
using SmartRecruitment.API.Models.DTOs.Admin;
using SmartRecruitment.API.Models.Entities;

namespace SmartRecruitment.API.Mappings
{
    public class AdminMappingProfile : Profile
    {
        public AdminMappingProfile()
        {
            CreateMap<User, UserAccountResponseDto>();
        }
    }
}
