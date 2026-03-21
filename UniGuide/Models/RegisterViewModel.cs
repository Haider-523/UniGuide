using System.ComponentModel.DataAnnotations;

namespace UniGuide.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string? FullName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, MinLength(6)]
        public string? Password { get; set; }

        [Required, Compare("Password")]
        public string? ConfirmPassword { get; set; }

        [Required]
        public string? InterGroup { get; set; }

        [Range(33, 100)]
        public double InterMarksPercent { get; set; }

        [Required]
        public string? City { get; set; }

        public decimal BudgetPerSemester { get; set; }
    }
}