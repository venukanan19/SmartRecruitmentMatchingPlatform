namespace SmartRecruitment.API.Constants
{
    public class CvStorageConstants
    {
        public const string StorageConfigurationKey =
            "CvStorage:RootPath";

        public const long MaximumFileSizeBytes =
            5 * 1024 * 1024;

        public static readonly IReadOnlySet<string>
            AllowedExtensions =
                new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase)
                {
                    ".pdf",
                    ".docx"
                };

        public static readonly IReadOnlySet<string>
            AllowedMimeTypes =
                new HashSet<string>(
                    StringComparer.OrdinalIgnoreCase)
                {
                    "application/pdf",
                    "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                };
    }
}
