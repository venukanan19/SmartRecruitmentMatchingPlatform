using SmartRecruitment.API.Models.Common;
using SmartRecruitment.API.Models.DTOs.JobSeeker;

namespace SmartRecruitment.API.Services.Interfaces
{
    public interface ICvStorageService
    {
        Task<CvUploadResponseDto>
            UploadOrReplaceAsync(
                int userId,
                IFormFile file);

        Task<CvMetadataResponseDto?>
            GetMetadataAsync(
                int userId);

        Task<FileStreamResultData?>
            GetContentAsync(
                int userId);
    }
}

