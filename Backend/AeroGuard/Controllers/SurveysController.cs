using AeroGuard.DTOs.Request;
using AeroGuard.DTOs.Response;
using AeroGuard.Models;
using AeroGuard.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeroGuard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveyRepository _repository;

        public SurveysController(ISurveyRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var surveys = await _repository.GetAllAsync();

            var response = surveys.Select(x => new
            {
                x.SurveyID,
                x.SurveyType,
                x.AreaName,
                x.SurveyDate,
                x.SurveyStatus
            });

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var survey = await _repository.GetByIdAsync(id);

            if (survey == null)
                return NotFound();

            return Ok(survey);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateSurveyDto request)
        {
            string type = request.SurveyType
                .Trim()
                .ToUpperInvariant();

            if (type != "MOSQUITO" && type != "FARMING")
                return BadRequest("Invalid survey type.");

            if (string.IsNullOrWhiteSpace(request.AreaName))
                return BadRequest("Area name is required.");

            var survey = new Survey
            {
                SurveyType = type,
                AreaName = request.AreaName.Trim(),
                Notes = request.Notes
            };

            await _repository.AddAsync(survey);

            return CreatedAtAction(
                nameof(GetById),
                new { id = survey.SurveyID },
                new SurveyResponseDto
                {
                    SurveyID = survey.SurveyID,
                    SurveyType = survey.SurveyType,
                    AreaName = survey.AreaName,
                    SurveyStatus = survey.SurveyStatus,
                    SurveyDate = survey.SurveyDate
                });
        }
    }
}
