using AeroGuard.DTOs.Request;
using AeroGuard.Repositories.Interface;
using AeroGuard.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroGuard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        private readonly IImageRepository _images;
        private readonly MockAIService _mockAI;

        public AnalysisController(
            IImageRepository images,
            MockAIService mockAI)
        {
            _images = images;
            _mockAI = mockAI;
        }

        [HttpPost("mock")]
        public async Task<IActionResult> AnalyzeMock(
            [FromBody] MockAnalysisRequestDto request)
        {
            var image = await _images.GetByIdAsync(
                request.ImageID);

            if (image == null)
            {
                return NotFound("Image not found.");
            }

            var result = _mockAI.Analyze(request.ImageID);

            return Ok(result);
        }
    }
}
