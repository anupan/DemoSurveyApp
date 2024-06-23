using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SurveyAPI.Models
{
    public class SurveyAnswer
    {
        [Key]
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        [Required]
        public string? AnswerText { get; set; }
    }
}
