using AeroGuard.Data;
using AeroGuard.Models;
using AeroGuard.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AeroGuard.Repositories.Implementation
{
    public class AnalysisRepository : IAnalysisRepository
    {
        private readonly AeroGuardDbContext _context;

        public AnalysisRepository(AeroGuardDbContext context)
        {
            _context = context;
        }

        public async Task<AIAnalysisRun> AddAsync(
            AIAnalysisRun analysisRun)
        {
            await _context.AIAnalysisRuns.AddAsync(analysisRun);

            await _context.SaveChangesAsync();

            return analysisRun;
        }

        public async Task<AIAnalysisRun?> GetByIdAsync(
            int analysisRunId)
        {
            return await _context.AIAnalysisRuns
                .AsNoTracking()
                .Include(x => x.Findings)
                .FirstOrDefaultAsync(
                    x => x.AnalysisRunID == analysisRunId);
        }

        public async Task<List<AIAnalysisRun>> GetByImageIdAsync(
            int imageId)
        {
            return await _context.AIAnalysisRuns
                .AsNoTracking()
                .Include(x => x.Findings)
                .Where(x => x.ImageID == imageId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}

