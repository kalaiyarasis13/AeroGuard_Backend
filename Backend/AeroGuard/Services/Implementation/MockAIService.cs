using AeroGuard.DTOs.Response;

namespace AeroGuard.Services.Implementation
{
    public class MockAIService
    {
        public MosquitoAnalysisResponseDto Analyze(int imageId)
        {
            return new MosquitoAnalysisResponseDto
            {
                ImageID = imageId,

                AnalysisType = "MOSQUITO",

                ResultSource = "MOCK",

                Status = "COMPLETED",

                ModelVersion = "mock-v1",

                Detections = new List<MosquitoDetectionDto>
                {
                    new MosquitoDetectionDto
                    {
                        ClassName = "candidate_water_holding_site",
                        Confidence = 0.85,

                        BoundingBox = new int[]
                        {
                            10, 10, 100, 100
                        }
                    }
                }
            };
        }
    }
}
