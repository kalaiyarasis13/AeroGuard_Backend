using AeroGuard.DTOs.Request;
using AeroGuard.Models;
using AeroGuard.Repositories.Interface;
using AeroGuard.Services.Implementation;
using AeroGuard.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroGuard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        private readonly IAnalysisService _analysisService;

        public AnalysisController(
            IAnalysisService analysisService)
        {
            _analysisService = analysisService;
        }

        // POST: api/Analysis/mock
        [HttpPost("mock")]
        public async Task<IActionResult> RunMock(
            [FromBody] MockAnalysisRequestDto request)
        {
            var result = await _analysisService.RunMockAsync(
                request.ImageID);

            if (result == null)
            {
                return NotFound("Image not found.");
            }

            return Ok(ToResponse(result));
        }

        // GET: api/Analysis/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _analysisService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound("Analysis not found.");
            }

            return Ok(ToResponse(result));
        }

        // GET: api/Analysis/image/1
        [HttpGet("image/{imageId:int}")]
        public async Task<IActionResult> GetByImageId(
            int imageId)
        {
            var results = await _analysisService
                .GetByImageIdAsync(imageId);

            return Ok(results.Select(ToResponse));
        }

        private static object ToResponse(AIAnalysisRun run)
        {
            return new
            {
                run.AnalysisRunID,
                run.ImageID,
                run.AnalysisType,
                run.ResultSource,
                run.Status,
                run.ModelVersion,
                run.CreatedAt,

                Detections = run.Findings.Select(f => new
                {
                    f.FindingID,
                    f.ClassName,
                    f.Confidence,

                    BoundingBox = new[]
                    {
                        f.X1,
                        f.Y1,
                        f.X2,
                        f.Y2
                    }
                })
            };
        }
    }
}
