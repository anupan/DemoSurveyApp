using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SurveyAPI.Constants;

namespace SurveyAPI.Models
{
    public class SurveyQuestion
    {
        [Key]
        public int QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public AnswerType AnswerType { get; set; }
    }
}
