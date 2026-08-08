using SmartRecruitment.API.Constants;

namespace SmartRecruitment.API.Helpers
{
    public class SafeFileNameGenerator
    {
        public string Generate(
            string originalFileName)
        {
            if (string.IsNullOrWhiteSpace(
                originalFileName))
            {
                throw new ArgumentException(
                    "A valid filename is required.",
                    nameof(originalFileName));
            }

            var fileName =
                Path.GetFileName(
                    originalFileName);

            var extension =
                Path.GetExtension(
                    fileName);

            if (string.IsNullOrWhiteSpace(extension) ||
                !CvStorageConstants
                    .AllowedExtensions
                    .Contains(extension))
            {
                throw new InvalidDataException(
                    "The CV file extension is not allowed.");
            }

            return
                $"{Guid.NewGuid():N}" +
                extension.ToLowerInvariant();
        }
    }
}
