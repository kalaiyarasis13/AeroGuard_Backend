using AeroGuard.DTOs.Response;
using AeroGuard.Models;
using AeroGuard.Repositories.Interface;
using AeroGuard.Services.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroGuard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ISurveyRepository _surveys;
        private readonly IImageRepository _images;
        private readonly ImageStorageService _storage;

        public ImagesController(
            ISurveyRepository surveys,
            IImageRepository images,
            ImageStorageService storage)
        {
            _surveys = surveys;
            _images = images;
            _storage = storage;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload(
        [FromQuery] int surveyId,
        IFormFile file)
        {
            if (!await _surveys.ExistsAsync(surveyId))
                return NotFound("Survey not found.");

            if (file == null)
                return BadRequest("Image required.");

            string? savedPath = null;

            try
            {
                var saved = await _storage.SaveAsync(file);
                savedPath = saved.Path;

                var image = new SurveyImage
                {
                    SurveyID = surveyId,
                    OriginalFileName = Path.GetFileName(
                        file.FileName),
                    FilePath = saved.Path,
                    FileSizeBytes = saved.Size
                };

                await _images.AddAsync(image);

                return Ok(new ImageUploadResponseDto
                {
                    ImageID = image.ImageID,
                    SurveyID = image.SurveyID,
                    OriginalFileName = image.OriginalFileName,
                    FileSizeBytes = image.FileSizeBytes
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                if (savedPath != null)
                    _storage.Delete(savedPath);

                return StatusCode(
                    500, "Image upload failed.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetImages(int surveyId)
        {
            if (!await _surveys.ExistsAsync(surveyId))
                return NotFound("Survey not found.");

            var images = await _images.GetBySurveyAsync(
                surveyId);

            return Ok(images.Select(x => new
            {
                x.ImageID,
                x.SurveyID,
                x.OriginalFileName,
                x.FileSizeBytes,
                x.UploadedAt
            }));
        }
    }
}
