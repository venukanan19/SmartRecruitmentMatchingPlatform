using AutoMapper;
using SmartRecruitment.API.Models.DTOs.Notification;
using SmartRecruitment.API.Models.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartRecruitment.API.Mappings
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<Notification, NotificationResponseDto>();
        }
    }
}