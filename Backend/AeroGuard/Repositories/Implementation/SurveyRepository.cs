using AeroGuard.Data;
using AeroGuard.Models;
using AeroGuard.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AeroGuard.Repositories.Implementation
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly AeroGuardDbContext _context;

        public SurveyRepository(AeroGuardDbContext context)
        {
            _context = context;
        }

        public async Task<List<Survey>> GetAllAsync()
        {
            return await _context.Surveys
                .AsNoTracking()
                .OrderByDescending(x => x.SurveyDate)
                .ToListAsync();
        }

        public async Task<Survey?> GetByIdAsync(int id)
        {
            return await _context.Surveys
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.SurveyID == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Surveys
                .AnyAsync(x => x.SurveyID == id);
        }

        public async Task<Survey> AddAsync(Survey survey)
        {
            _context.Surveys.Add(survey);

            await _context.SaveChangesAsync();

            return survey;
        }
    }
}
