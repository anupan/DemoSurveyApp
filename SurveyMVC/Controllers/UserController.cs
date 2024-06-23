using Microsoft.AspNetCore.Mvc;
using SurveyAPI.Models;
using SurveyMVC.ViewModels;

namespace SurveyMVC.Controllers;

public class UserController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UserController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _httpClientFactory.CreateClient("SurveyAPI");

        var response = await client.GetAsync("/api/survey/questions");
        var questions = await response.Content.ReadFromJsonAsync<IEnumerable<SurveyQuestionData>>();
        return View(questions);
    }

    [HttpPost]
    public async Task<IActionResult> SubmitSurvey(List<SurveyAnswer> answers)
    {
        if (answers == null || !answers.Any())
        {
            return View();
        }

        if(ModelState.IsValid)
            {
            var client = _httpClientFactory.CreateClient("SurveyAPI");

            foreach (var answer in answers)
            {
                await client.PostAsJsonAsync("/api/survey/answers", answer);
            }

            TempData["SuccessMessage"] = "Your survey answers have been successfully submitted.";
        }

        return RedirectToAction("Index");
    }
}
