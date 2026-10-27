namespace AeroGuard.Models
{
    public class SurveyImage
    {
        public int ImageID { get; set; }

        public int SurveyID { get; set; }

        public string OriginalFileName { get; set; } = "";

        public string FilePath { get; set; } = "";

        public long FileSizeBytes { get; set; }

        public DateTime UploadedAt { get; set; }
            = DateTime.UtcNow;

        public Survey Survey { get; set; } = null!;
    }
}
