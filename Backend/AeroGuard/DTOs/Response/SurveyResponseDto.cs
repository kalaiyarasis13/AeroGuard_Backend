namespace AeroGuard.DTOs.Response
{
    public class SurveyResponseDto
    {
        public int SurveyID { get; set; }

        public string SurveyType { get; set; } = "";

        public string AreaName { get; set; } = "";

        public string SurveyStatus { get; set; } = "";

        public DateTime SurveyDate { get; set; }
    }
}
