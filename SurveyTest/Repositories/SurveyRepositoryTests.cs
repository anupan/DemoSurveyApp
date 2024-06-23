using SurveyAPI.Constants;
using SurveyAPI.Models;
using SurveyAPI.Repositories;

namespace SurveyTest.Services
{
    public class SurveyRepositoryTests : IClassFixture<TestSurveyDbContext>
    {
        private readonly TestSurveyDbContext _surveyDbContext;
        private readonly SurveyRepository _repository;

        public SurveyRepositoryTests(TestSurveyDbContext surveyDbContext)
        {
            _surveyDbContext = surveyDbContext;
            _repository = new SurveyRepository(_surveyDbContext.Context);
        }

        [Fact]
        public void GetQuestions_ReturnsAllQuestions()
        {
            // Arrange
            using (var context = _surveyDbContext.CreateContext())
            {
                context.SurveyQuestions.Add(new SurveyQuestion { QuestionText = "Question 1", AnswerType = AnswerType.FreeText });
                context.SurveyQuestions.Add(new SurveyQuestion { QuestionText = "Question 2", AnswerType = AnswerType.YesNo });
                context.SaveChanges();
            }

            // Act
            List<SurveyQuestion> questions = _repository.GetAllQuestions().ToList();

            // Assert
            Assert.Equal(2, questions.Count);
            Assert.Contains(questions, x => x.QuestionText == "Question 1" && x.AnswerType == AnswerType.FreeText);
            Assert.Contains(questions, x => x.QuestionText == "Question 2" && x.AnswerType == AnswerType.YesNo);
        }

        [Fact]
        public void AddQuestion_Successfully()
        {
            // Arrange
            var newQuestion = new SurveyQuestion { QuestionText = "Test Question", AnswerType = AnswerType.FreeText };

            // Act
            _repository.AddQuestion(newQuestion);

            // Assert
            using (var context = _surveyDbContext.CreateContext())
            {
                var addedQuestion = context.SurveyQuestions.FirstOrDefault(x => x.QuestionText == "Test Question" && x.AnswerType == AnswerType.FreeText);
                Assert.NotNull(addedQuestion);
                Assert.Equal(AnswerType.FreeText, addedQuestion.AnswerType);
                Assert.Equal("Test Question", addedQuestion.QuestionText);
            }
        }

        [Fact]
        public void AddAnswer_Successfully()
        {
            // Arrange
            var newAnswer = new SurveyAnswer { QuestionId = 1, AnswerText = "Test Answer" };

            // Act
            _repository.AddAnswer(newAnswer);

            // Assert
            using (var context = _surveyDbContext.CreateContext())
            {
                var addedAnswer = context.SurveyAnswers.FirstOrDefault(x => x.AnswerText == "Test Answer" && x.QuestionId == 1);
                Assert.NotNull(addedAnswer);
                Assert.Equal(1, addedAnswer.QuestionId);
                Assert.Equal("Test Answer", addedAnswer.AnswerText);
            }
        }
    }
}