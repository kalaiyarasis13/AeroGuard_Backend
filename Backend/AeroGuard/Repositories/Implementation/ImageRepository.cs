using AeroGuard.Data;
using AeroGuard.Models;
using AeroGuard.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AeroGuard.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly AeroGuardDbContext _context;

        public ImageRepository(AeroGuardDbContext context)
        {
            _context = context;
        }

        public async Task<SurveyImage> AddAsync(
            SurveyImage image)
        {
            _context.SurveyImages.Add(image);

            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<List<SurveyImage>> GetBySurveyAsync(
            int surveyId)
        {
            return await _context.SurveyImages
                .AsNoTracking()
                .Where(x => x.SurveyID == surveyId)
                .OrderByDescending(x => x.UploadedAt)
                .ToListAsync();
        }
    }
}
