using System.ComponentModel.DataAnnotations;

namespace AeroGuard.Models
{
    public class AIFinding
    {
        [Key]
        public int FindingID { get; set; }

        [Required]
        public int AnalysisRunID { get; set; }

        [Required]
        [MaxLength(100)]
        public string ClassName { get; set; } = "";

        [Range(0.0, 1.0)]
        public double Confidence { get; set; }

        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }

        public AIAnalysisRun? AnalysisRun { get; set; }
    }
}

