using System.ComponentModel.DataAnnotations;

namespace AeroGuard.DTOs.Request
{
    public class CreateSurveyDto
    {
        [Required]
        public string SurveyType { get; set; } = "";

        [Required]
        public string AreaName { get; set; } = "";

        public string? Notes { get; set; }
    }
}
