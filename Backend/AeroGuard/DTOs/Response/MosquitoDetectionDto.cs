namespace AeroGuard.DTOs.Response
{
    public class MosquitoDetectionDto
    {
        public string ClassName { get; set; } = "";

        public double Confidence { get; set; }

        public int[] BoundingBox { get; set; } =
            Array.Empty<int>();
    }
}
