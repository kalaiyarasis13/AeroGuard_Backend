using AeroGuard.Models;

namespace AeroGuard.Repositories.Interface
{
    public interface ISurveyRepository
    {
        Task<List<Survey>> GetAllAsync();

        Task<Survey?> GetByIdAsync(int id);

        Task<Survey> AddAsync(Survey survey);

        Task<bool> ExistsAsync(int id);
    }
}
