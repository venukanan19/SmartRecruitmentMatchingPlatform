namespace SmartRecruitment.API.Models.Common
{
    public class FileStreamResultData
    {
        public required Stream Stream
        {
            get;
            init;
        }

        public required string ContentType
        {
            get;
            init;
        }

        public required string DownloadFileName
        {
            get;
            init;
        }
    }
}
