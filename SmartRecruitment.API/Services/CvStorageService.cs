//using SmartRecruitment.API.Constants;
//using SmartRecruitment.API.Helpers;
//using SmartRecruitment.API.Models.Common;
//using SmartRecruitment.API.Models.DTOs.JobSeeker;
//using SmartRecruitment.API.Models.Entities;
//using SmartRecruitment.API.Repositories;
//using SmartRecruitment.API.Repositories.Interfaces;
//using SmartRecruitment.API.Services.Interfaces;

//namespace SmartRecruitment.API.Services
//{
//    public class CvStorageService
//    : ICvStorageService
//    {
//        private readonly IJobSeekerRepository
//            _jobSeekerRepository;

//        private readonly ICvMetadataRepository
//            _metadataRepository;

//        private readonly FileValidationHelper
//            _fileValidationHelper;

//        private readonly SafeFileNameGenerator
//            _fileNameGenerator;

//        private readonly IMapper _mapper;

//        private readonly string _storageRoot;

//        public CvStorageService(
//            IJobSeekerRepository jobSeekerRepository,
//            ICvMetadataRepository metadataRepository,
//            FileValidationHelper fileValidationHelper,
//            SafeFileNameGenerator fileNameGenerator,
//            IConfiguration configuration,
//            IWebHostEnvironment environment,
//            IMapper mapper)
//        {
//            _jobSeekerRepository =
//                jobSeekerRepository;

//            _metadataRepository =
//                metadataRepository;

//            _fileValidationHelper =
//                fileValidationHelper;

//            _fileNameGenerator =
//                fileNameGenerator;

//            _mapper =
//                mapper;

//            var configuredPath =
//                configuration[
//                    CvStorageConstants
//                        .StorageConfigurationKey];

//            var selectedPath =
//                string.IsNullOrWhiteSpace(
//                    configuredPath)
//                    ? Path.Combine(
//                        environment.ContentRootPath,
//                        "Storage",
//                        "CVs")
//                    : configuredPath;

//            _storageRoot =
//                Path.GetFullPath(
//                    selectedPath);

//            EnsureStorageIsOutsideWebRoot(
//                environment.WebRootPath);

//            Directory.CreateDirectory(
//                _storageRoot);
//        }

//        public async Task<CvUploadResponseDto>
//            UploadOrReplaceAsync(
//                int userId,
//                IFormFile file)
//        {
//            var profile =
//                await _jobSeekerRepository
//                    .GetByUserIdAsync(
//                        userId);

//            if (profile is null)
//            {
//                throw new KeyNotFoundException(
//                    "Create the Job Seeker profile before uploading a CV.");
//            }

//            _fileValidationHelper
//                .Validate(file);

//            var safeStoredFileName =
//                _fileNameGenerator
//                    .Generate(
//                        file.FileName);

//            var newPhysicalPath =
//                GetSafePhysicalPath(
//                    safeStoredFileName);

//            var existingMetadata =
//                await _metadataRepository
//                    .GetByUserIdAsync(
//                        userId);

//            var oldStoredFileName =
//                existingMetadata?
//                    .StoredFileName;

//            var newFileWasSaved =
//                false;

//            try
//            {
//                await using (
//                    var stream =
//                        new FileStream(
//                            newPhysicalPath,
//                            FileMode.CreateNew,
//                            FileAccess.Write,
//                            FileShare.None,
//                            81920,
//                            true))
//                {
//                    await file
//                        .CopyToAsync(
//                            stream);
//                }

//                newFileWasSaved =
//                    true;

//                if (existingMetadata is null)
//                {
//                    existingMetadata =
//                        new CvMetadata
//                        {
//                            JobSeekerProfileId =
//                                profile
//                                    .JobSeekerProfileId,

//                            OriginalFileName =
//                                Path.GetFileName(
//                                    file.FileName),

//                            StoredFileName =
//                                safeStoredFileName,

//                            ContentType =
//                                file.ContentType,

//                            FileSize =
//                                file.Length,

//                            UploadedAt =
//                                DateTime.UtcNow
//                        };

//                    await _metadataRepository
//                        .AddAsync(
//                            existingMetadata);
//                }
//                else
//                {
//                    existingMetadata
//                        .OriginalFileName =
//                            Path.GetFileName(
//                                file.FileName);

//                    existingMetadata
//                        .StoredFileName =
//                            safeStoredFileName;

//                    existingMetadata
//                        .ContentType =
//                            file.ContentType;

//                    existingMetadata
//                        .FileSize =
//                            file.Length;

//                    existingMetadata
//                        .UploadedAt =
//                            DateTime.UtcNow;

//                    _metadataRepository
//                        .Update(
//                            existingMetadata);
//                }

//                var metadataSaved =
//                    await _metadataRepository
//                        .SaveChangesAsync();

//                if (!metadataSaved)
//                {
//                    throw new InvalidOperationException(
//                        "The CV metadata could not be saved.");
//                }

//                DeletePreviousFile(
//                    oldStoredFileName,
//                    safeStoredFileName);

//                var response =
//                    _mapper.Map<
//                        CvUploadResponseDto>(
//                            existingMetadata);

//                response.Message =
//                    oldStoredFileName is null
//                        ? "CV uploaded successfully."
//                        : "CV replaced successfully.";

//                return response;
//            }
//            catch
//            {
//                if (newFileWasSaved &&
//                    File.Exists(
//                        newPhysicalPath))
//                {
//                    try
//                    {
//                        File.Delete(
//                            newPhysicalPath);
//                    }
//                    catch
//                    {
//                        // Log through the approved
//                        // shared logging infrastructure.
//                    }
//                }

//                throw;
//            }
//        }

//        public async Task<CvMetadataResponseDto?>
//            GetMetadataAsync(
//                int userId)
//        {
//            var metadata =
//                await _metadataRepository
//                    .GetByUserIdAsync(
//                        userId);

//            return metadata is null
//                ? null
//                : _mapper.Map<
//                    CvMetadataResponseDto>(
//                        metadata);
//        }

//        public async Task<FileStreamResultData?>
//            GetContentAsync(
//                int userId)
//        {
//            var metadata =
//                await _metadataRepository
//                    .GetByUserIdAsync(
//                        userId);

//            if (metadata is null)
//            {
//                return null;
//            }

//            var physicalPath =
//                GetSafePhysicalPath(
//                    metadata.StoredFileName);

//            if (!File.Exists(
//                physicalPath))
//            {
//                return null;
//            }

//            var stream =
//                new FileStream(
//                    physicalPath,
//                    FileMode.Open,
//                    FileAccess.Read,
//                    FileShare.Read,
//                    81920,
//                    true);

//            return new FileStreamResultData
//            {
//                Stream =
//                    stream,

//                ContentType =
//                    metadata.ContentType,

//                DownloadFileName =
//                    metadata.OriginalFileName
//            };
//        }

//        private string GetSafePhysicalPath(
//            string storedFileName)
//        {
//            var fileNameOnly =
//                Path.GetFileName(
//                    storedFileName);

//            if (!string.Equals(
//                fileNameOnly,
//                storedFileName,
//                StringComparison.Ordinal))
//            {
//                throw new InvalidDataException(
//                    "The stored filename is invalid.");
//            }

//            var fullPath =
//                Path.GetFullPath(
//                    Path.Combine(
//                        _storageRoot,
//                        fileNameOnly));

//            var expectedPrefix =
//                _storageRoot.EndsWith(
//                    Path.DirectorySeparatorChar)
//                    ? _storageRoot
//                    : _storageRoot +
//                      Path.DirectorySeparatorChar;

//            if (!fullPath.StartsWith(
//                expectedPrefix,
//                StringComparison.OrdinalIgnoreCase))
//            {
//                throw new InvalidDataException(
//                    "The requested CV file path is invalid.");
//            }

//            return fullPath;
//        }

//        private void DeletePreviousFile(
//            string? previousStoredFileName,
//            string newStoredFileName)
//        {
//            if (string.IsNullOrWhiteSpace(
//                    previousStoredFileName) ||
//                string.Equals(
//                    previousStoredFileName,
//                    newStoredFileName,
//                    StringComparison.OrdinalIgnoreCase))
//            {
//                return;
//            }

//            var previousPath =
//                GetSafePhysicalPath(
//                    previousStoredFileName);

//            if (File.Exists(
//                previousPath))
//            {
//                File.Delete(
//                    previousPath);
//            }
//        }

//        private void EnsureStorageIsOutsideWebRoot(
//            string? webRootPath)
//        {
//            if (string.IsNullOrWhiteSpace(
//                webRootPath))
//            {
//                return;
//            }

//            var fullWebRoot =
//                Path.GetFullPath(
//                    webRootPath);

//            var webRootPrefix =
//                fullWebRoot.EndsWith(
//                    Path.DirectorySeparatorChar)
//                    ? fullWebRoot
//                    : fullWebRoot +
//                      Path.DirectorySeparatorChar;

//            if (_storageRoot.StartsWith(
//                webRootPrefix,
//                StringComparison.OrdinalIgnoreCase))
//            {
//                throw new InvalidOperationException(
//                    "CV storage must be outside wwwroot.");
//            }
//        }
//    }
//}
