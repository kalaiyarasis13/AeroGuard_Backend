namespace AeroGuard.DTOs.Response
{
    public class ImageUploadResponseDto
    {
        public int ImageID { get; set; }

        public int SurveyID { get; set; }

        public string OriginalFileName { get; set; } = "";

        public long FileSizeBytes { get; set; }

        public string Status { get; set; } = "UPLOADED";
    }
}
