using System.ComponentModel.DataAnnotations;

namespace UniGuide.Models
{
    public class StudentQuizResult
    {
        [Key]
        public int ResultID { get; set; }
        public string? InterestProfile { get; set; }
        public string? RecommendedField { get; set; }
        public DateTime TakenAt { get; set; } = DateTime.Now;
        public string? ScoreBreakDown { get; set; }
        public int StudentID { get; set; }
        public Student? Student { get; set; }
    }
}
