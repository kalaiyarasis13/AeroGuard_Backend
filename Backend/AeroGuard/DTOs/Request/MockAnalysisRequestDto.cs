using System.ComponentModel.DataAnnotations;

namespace AeroGuard.DTOs.Request
{
    public class MockAnalysisRequestDto
    {
        [Range(1, int.MaxValue)]
        public int ImageID { get; set; }
    }
}
