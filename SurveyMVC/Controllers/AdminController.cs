using Microsoft.AspNetCore.Mvc;
using SurveyAPI.Models;
using SurveyMVC.ViewModels;

namespace SurveyMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SurveyQuestionData surveyQuestion)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient("SurveyAPI");
                await client.PostAsJsonAsync("/api/survey/questions", surveyQuestion);
                return RedirectToAction("Index", "User");
            }

            return View(surveyQuestion);
        }
        public async Task<IActionResult> SurveySummary()
        {
            var client = _httpClientFactory.CreateClient("SurveyAPI");

            var response = await client.GetAsync("/api/survey/surveysummary");
            if (response.IsSuccessStatusCode)
            {
                var surveySummary = await response.Content.ReadFromJsonAsync<SurveySummaryData>();
                return View(surveySummary);
            }

            return View();
        }
    }
}
