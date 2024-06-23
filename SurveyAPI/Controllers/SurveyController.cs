using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyAPI.Models;
using SurveyAPI.Services;

namespace SurveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _service;

        public SurveyController(ISurveyService service)
        {
            _service = service;
        }

        [HttpGet("questions")]
        public IActionResult GetQuestions()
        {
            var questions = _service.GetQuestions();
            return Ok(questions);
        }

        [HttpPost("questions")]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddQuestion([FromBody] SurveyQuestion question)
        {
            _service.AddQuestion(question);
            return Ok();
        }

        [HttpPost("answers")]
        //[Authorize(Roles = "User")]
        public IActionResult AddAnswer([FromBody] SurveyAnswer answer)
        {
            _service.AddAnswer(answer);
            return Ok();
        }

        [HttpGet("surveysummary")]
        public IActionResult GetSurveySummary()
        {
            var surveySummary = _service.GetSurveySummary();
            return Ok(surveySummary);
        }
    }
}
