using SurveyAPI.Constants;
using System.ComponentModel.DataAnnotations;

namespace SurveyMVC.ViewModels
{
    public class SurveyQuestionData
    {
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Please enter the question text.")]
        public string? QuestionText { get; set; }
        public AnswerType AnswerType { get; set; }
    }
}
