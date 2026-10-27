using System.ComponentModel.DataAnnotations;

namespace AeroGuard.Models
{
    public class AIAnalysisRun
    {
        [Key]
        public int AnalysisRunID { get; set; }

        [Required]
        public int ImageID { get; set; }

        [Required]
        [MaxLength(30)]
        public string AnalysisType { get; set; } = "MOSQUITO";

        [Required]
        [MaxLength(20)]
        public string ResultSource { get; set; } = "MOCK";

        [Required]
        [MaxLength(30)]
        public string Status { get; set; } = "COMPLETED";

        [Required]
        [MaxLength(100)]
        public string ModelVersion { get; set; } = "mock-v1";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public SurveyImage? Image { get; set; }

        public ICollection<AIFinding> Findings { get; set; }
            = new List<AIFinding>();
    }
}

