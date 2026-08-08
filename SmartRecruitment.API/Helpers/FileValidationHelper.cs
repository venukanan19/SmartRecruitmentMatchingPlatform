using SmartRecruitment.API.Constants;

namespace SmartRecruitment.API.Helpers
{
    public class FileValidationHelper
    {
        public void Validate(
            IFormFile? file)
        {
            if (file is null)
            {
                throw new InvalidDataException(
                    "A CV file is required.");
            }

            if (file.Length <= 0)
            {
                throw new InvalidDataException(
                    "The CV file cannot be empty.");
            }

            if (file.Length >
                CvStorageConstants
                    .MaximumFileSizeBytes)
            {
                throw new InvalidDataException(
                    "The CV file exceeds the maximum permitted size.");
            }

            var extension =
                Path.GetExtension(
                    Path.GetFileName(
                        file.FileName));

            if (!CvStorageConstants
                .AllowedExtensions
                .Contains(extension))
            {
                throw new InvalidDataException(
                    "The CV file extension is not supported.");
            }

            if (string.IsNullOrWhiteSpace(
                    file.ContentType) ||
                !CvStorageConstants
                    .AllowedMimeTypes
                    .Contains(
                        file.ContentType))
            {
                throw new InvalidDataException(
                    "The CV content type is not supported.");
            }
        }
    }
}
