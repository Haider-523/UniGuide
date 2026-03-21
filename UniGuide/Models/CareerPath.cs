using System.ComponentModel.DataAnnotations;

namespace UniGuide.Models
{
    public class CareerPath
    {
        [Key]
        public int CareerID { get; set; }
        [Required]
        public string? JobTitle { get; set; }
        public decimal MinSalaryPKR { get; set; }
        public decimal MaxSalaryPKR { get; set; }
        public string? IndustryType { get; set; }
        public string? DemandLevel { get; set; }
        public string? SkillRequired { get; set; }
        public int ProgramID { get; set; }
        public Program Program { get; set; }
    }
}
