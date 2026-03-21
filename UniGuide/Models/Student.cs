using System.ComponentModel.DataAnnotations;

namespace UniGuide.Models
{
    public class Student
    {
        public int StudentID { get; set; }

        [Required, MaxLength(100)]
        public string? FullName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? InterGroup { get; set; }

        [Range(33, 100)]
        public double InterMarksPercent { get; set; }

        [Required]
        public string? City { get; set; }

        public decimal BudgetPerSemester { get; set; }

        public string? PasswordHash { get; set; }  // ✅ new

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<SavedUniversity>? SavedUniversities { get; set; }
        public ICollection<StudentQuizResult>? QuizResults { get; set; }
    }
}