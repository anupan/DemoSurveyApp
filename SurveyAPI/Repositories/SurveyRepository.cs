using SurveyAPI.Data;
using SurveyAPI.Models;

namespace SurveyAPI.Repositories
{
    public interface ISurveyRepository
    {
        IEnumerable<SurveyQuestion> GetAllQuestions();
        void AddQuestion(SurveyQuestion question);
        void AddAnswer(SurveyAnswer answer);
        IEnumerable<SurveyAnswer> GetAllAnswers();
    }
    public class SurveyRepository : ISurveyRepository
    {
        private readonly SurveyDbContext _context;

        public SurveyRepository(SurveyDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SurveyQuestion> GetAllQuestions()
        {
            return _context.SurveyQuestions.ToList();
        }

        public void AddQuestion(SurveyQuestion question)
        {
            _context.SurveyQuestions.Add(question);
            _context.SaveChanges();
        }

        public void AddAnswer(SurveyAnswer answer)
        {
            _context.SurveyAnswers.Add(answer);
            _context.SaveChanges();
        }

        public IEnumerable<SurveyAnswer> GetAllAnswers()
        {
            return _context.SurveyAnswers.ToList();
        }
    }
}
