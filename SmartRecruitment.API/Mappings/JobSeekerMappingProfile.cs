//using SmartRecruitment.API.Models.DTOs.JobSeeker;
//using SmartRecruitment.API.Models.Entities;
//using AutoMapper;

//namespace SmartRecruitment.API.Mappings
//{
//    public class JobSeekerMappingProfile
//    : Profile
//    {
//        public JobSeekerMappingProfile()
//        {
//            // =====================================================
//            // Entity -> Response DTO
//            // =====================================================

//            CreateMap<
//                JobSeekerProfile,
//                JobSeekerProfileResponseDto>()
//                .ForMember(
//                    destination => destination.Email,
//                    options => options.MapFrom(
//                        source =>
//                            source.User.Email))
//                .ForMember(
//                    destination => destination.Skills,
//                    options => options.MapFrom(
//                        source =>
//                            source.JobSeekerSkills))
//                .ForMember(
//                    destination => destination.Educations,
//                    options => options.MapFrom(
//                        source =>
//                            source.Educations))
//                .ForMember(
//                    destination => destination.Experiences,
//                    options => options.MapFrom(
//                        source =>
//                            source.Experiences))
//                .ForMember(
//                    destination => destination.CvMetadata,
//                    options => options.MapFrom(
//                        source =>
//                            source.CvMetadata));

//            CreateMap<
//                JobSeekerSkill,
//                JobSeekerSkillResponseDto>()
//                .ForMember(
//                    destination =>
//                        destination.SkillName,
//                    options => options.MapFrom(
//                        source =>
//                            source.Skill.SkillName));

//            CreateMap<
//                Education,
//                EducationResponseDto>();

//            CreateMap<
//                Experience,
//                ExperienceResponseDto>();

//            CreateMap<
//                CvMetadata,
//                CvMetadataResponseDto>();

//            CreateMap<
//                CvMetadata,
//                CvUploadResponseDto>()
//                .ForMember(
//                    destination => destination.Message,
//                    options => options.Ignore());

//            // =====================================================
//            // Request DTO -> Entity
//            // =====================================================

//            CreateMap<
//                UpdateJobSeekerProfileRequestDto,
//                JobSeekerProfile>()
//                .ForMember(
//                    destination =>
//                        destination.JobSeekerProfileId,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.UserId,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.CreatedAt,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.UpdatedAt,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.User,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.JobSeekerSkills,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.Educations,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.Experiences,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.CvMetadata,
//                    options => options.Ignore());

//            CreateMap<
//                CreateEducationRequestDto,
//                Education>()
//                .ForMember(
//                    destination =>
//                        destination.EducationId,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.JobSeekerProfileId,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.CreatedAt,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.JobSeekerProfile,
//                    options => options.Ignore());

//            CreateMap<
//                UpdateEducationRequestDto,
//                Education>()
//                .ForMember(
//                    destination =>
//                        destination.EducationId,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.JobSeekerProfileId,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.CreatedAt,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.JobSeekerProfile,
//                    options => options.Ignore());

//            CreateMap<
//                CreateExperienceRequestDto,
//                Experience>()
//                .ForMember(
//                    destination =>
//                        destination.ExperienceId,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.JobSeekerProfileId,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.CreatedAt,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.JobSeekerProfile,
//                    options => options.Ignore());

//            CreateMap<
//                UpdateExperienceRequestDto,
//                Experience>()
//                .ForMember(
//                    destination =>
//                        destination.ExperienceId,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.JobSeekerProfileId,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.CreatedAt,
//                    options => options.Ignore())
//                .ForMember(
//                    destination =>
//                        destination.JobSeekerProfile,
//                    options => options.Ignore());
//        }
//    }
//}
