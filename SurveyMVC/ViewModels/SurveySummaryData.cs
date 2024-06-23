using SurveyAPI.Constants;
using SurveyAPI.Models;

namespace SurveyMVC.ViewModels
{
    public class SurveySummaryData
    {
        public List<SummaryQuestionAnswers> QuestionAnswers { get; set; }
        public int TotalQuestions { get; set; }
        public int TotalAnswers { get; set; }
        public double ResponseRate { get; set; }
    }

    public class SummaryQuestionAnswers
    {
        public string? QuestionText { get; set; }
        public AnswerType AnswerType { get; set; }
        public List<SurveyAnswer> Answers { get; set; }
    }
}
