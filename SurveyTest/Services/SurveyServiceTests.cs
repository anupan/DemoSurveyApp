using Moq;
using SurveyAPI.Constants;
using SurveyAPI.Models;
using SurveyAPI.Repositories;
using SurveyAPI.Services;

namespace SurveyTest.Services
{
    public class SurveyServiceTests
    {
        private readonly Mock<ISurveyRepository> _mockRepo;
        private readonly ISurveyService _service;

        public SurveyServiceTests()
        {
            _mockRepo = new Mock<ISurveyRepository>();
            _service = new SurveyService(_mockRepo.Object);
        }

        [Fact]
        public void GetQuestions_ReturnsAllQuestions()
        {
            // Arrange
            var testQuestions = new List<SurveyQuestion>
            {
                new SurveyQuestion { QuestionId = 1, QuestionText = "Test Question 1", AnswerType = AnswerType.FreeText },
                new SurveyQuestion { QuestionId = 2, QuestionText = "Test Question 2", AnswerType = AnswerType.YesNo }
            };

            // Act
            _mockRepo.Setup(repo => repo.GetAllQuestions()).Returns(testQuestions);
            var questions = _service.GetQuestions();

            // Assert
            Assert.Equal(2, questions.Count());
        }

        [Fact]
        public void AddQuestions_Successfully()
        {
            // Arrange
            var testQuestion = new SurveyQuestion { QuestionText = "Test Question 1", AnswerType = AnswerType.FreeText };

            // Act
            _service.AddQuestion(testQuestion);

            // Assert
            _mockRepo.Verify(repo => repo.AddQuestion(It.Is<SurveyQuestion>(x =>
                x.QuestionText == "Test Question 1" && x.AnswerType == AnswerType.FreeText)), Times.Once);
        }

        [Fact]
        public void AddAnswer_Successfully()
        {
            // Arrange
            var testAnswer = new SurveyAnswer { QuestionId = 1, AnswerText = "Test Answer" };

            // Act
            _service.AddAnswer(testAnswer);

            // Assert
            _mockRepo.Verify(repo => repo.AddAnswer(It.Is<SurveyAnswer>(a =>
                a.QuestionId == 1 && a.AnswerText == "Test Answer")), Times.Once);
        }

    }
}