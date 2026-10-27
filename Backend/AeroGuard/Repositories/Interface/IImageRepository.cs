using AeroGuard.Models;

namespace AeroGuard.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<SurveyImage> AddAsync(SurveyImage image);

        Task<List<SurveyImage>> GetBySurveyAsync(int surveyId);
    }
}
