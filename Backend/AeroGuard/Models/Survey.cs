
namespace AeroGuard.Models
{
    public class Survey
    {
        public int SurveyID { get; set; }

        public string SurveyType { get; set; } = "";

        public string AreaName { get; set; } = "";

        public DateTime SurveyDate { get; set; }
            = DateTime.UtcNow;

        public string SurveyStatus { get; set; }
            = "CREATED";

        public string? Notes { get; set; }

        public ICollection<SurveyImage> Images { get; set; }
            = new List<SurveyImage>();
    }
}
