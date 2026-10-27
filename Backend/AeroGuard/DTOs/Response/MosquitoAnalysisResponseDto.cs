namespace AeroGuard.DTOs.Response
{
    public class MosquitoAnalysisResponseDto
    {
        public int ImageID { get; set; }

        public string AnalysisType { get; set; } =
            "MOSQUITO";

        public string ResultSource { get; set; } =
            "MOCK";

        public string Status { get; set; } =
            "COMPLETED";

        public string ModelVersion { get; set; } =
            "mock-v1";

        public List<MosquitoDetectionDto> Detections
        { get; set; } = new();
    }
}
