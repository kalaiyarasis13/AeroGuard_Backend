using AeroGuard.Models;
using AeroGuard.Repositories.Interface;
using AeroGuard.Services.Interface;

namespace AeroGuard.Services.Implementation
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IImageRepository _images;
        private readonly IAnalysisRepository _analyses;
        private readonly MockAIService _mockAI;

        public AnalysisService(
            IImageRepository images,
            IAnalysisRepository analyses,
            MockAIService mockAI)
        {
            _images = images;
            _analyses = analyses;
            _mockAI = mockAI;
        }

        public async Task<AIAnalysisRun?> RunMockAsync(int imageId)
        {
            var image = await _images.GetByIdAsync(imageId);

            if (image == null)
                return null;

            var mockResult = _mockAI.Analyze(imageId);

            var analysisRun = new AIAnalysisRun
            {
                ImageID = imageId,
                AnalysisType = "MOSQUITO",
                ResultSource = "MOCK",
                Status = "COMPLETED",
                ModelVersion = "mock-v1",
                CreatedAt = DateTime.UtcNow,

                Findings = mockResult.Detections.Select(d =>
                    new AIFinding
                    {
                        ClassName = d.ClassName,
                        Confidence = d.Confidence,
                        X1 = d.BoundingBox[0],
                        Y1 = d.BoundingBox[1],
                        X2 = d.BoundingBox[2],
                        Y2 = d.BoundingBox[3]
                    }).ToList()
            };

            return await _analyses.AddAsync(analysisRun);
        }

        public Task<AIAnalysisRun?> GetByIdAsync(
            int analysisRunId)
        {
            return _analyses.GetByIdAsync(analysisRunId);
        }

        public Task<List<AIAnalysisRun>> GetByImageIdAsync(
            int imageId)
        {
            return _analyses.GetByImageIdAsync(imageId);
        }
    }
}

