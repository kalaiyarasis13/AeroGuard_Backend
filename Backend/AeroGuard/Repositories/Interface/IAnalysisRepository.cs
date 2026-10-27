using AeroGuard.Models;

namespace AeroGuard.Repositories.Interface
{
    public interface IAnalysisRepository
    {
        Task<AIAnalysisRun> AddAsync(
            AIAnalysisRun analysisRun);

        Task<AIAnalysisRun?> GetByIdAsync(
            int analysisRunId);

        Task<List<AIAnalysisRun>> GetByImageIdAsync(
            int imageId);
    }
}
