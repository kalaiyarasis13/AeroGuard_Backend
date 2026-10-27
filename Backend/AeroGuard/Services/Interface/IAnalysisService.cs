using AeroGuard.Models;

namespace AeroGuard.Services.Interface
{
    public interface IAnalysisService
    {
        Task<AIAnalysisRun?> RunMockAsync(int imageId);

        Task<AIAnalysisRun?> GetByIdAsync(
            int analysisRunId);

        Task<List<AIAnalysisRun>> GetByImageIdAsync(
            int imageId);
    }
}
