using Microsoft.EntityFrameworkCore;
using SurveyAPI.Models;
using SurveyAPI.Repositories;

namespace SurveyAPI.Services
{
    public interface ISurveyService
    {
        IEnumerable<SurveyQuestion> GetQuestions();
        void AddQuestion(SurveyQuestion question);
        void AddAnswer(SurveyAnswer answer);
        SurveySummary GetSurveySummary();
    }
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _repository;

        public SurveyService(ISurveyRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<SurveyQuestion> GetQuestions()
        {
            return _repository.GetAllQuestions();
        }

        public void AddQuestion(SurveyQuestion question)
        {
            _repository.AddQuestion(question);
        }

        public void AddAnswer(SurveyAnswer answer)
        {
            _repository.AddAnswer(answer);
        }

        public SurveySummary GetSurveySummary()
        {
            var summary = new SurveySummary();
            var surveyQuestions = _repository.GetAllQuestions();
            var surveyAnswers = _repository.GetAllAnswers();

            // Count total number of questions
            summary.TotalQuestions = surveyQuestions.Count();
            // Count total number of answers
            summary.TotalAnswers = surveyAnswers.Count();
            // Calculate response rate
            if (summary.TotalAnswers > 0)
            {
                summary.ResponseRate = (double)summary.TotalAnswers / summary.TotalQuestions * 100;
            }

            summary.QuestionAnswers = surveyQuestions.Select(x => new SummaryQuestionAnswers
            {
                QuestionText = x.QuestionText,
                AnswerType = x.AnswerType,
                Answers = surveyAnswers.Where(a => a.QuestionId == x.QuestionId).ToList()
            }).ToList();

            return summary;
        }
    }
}
