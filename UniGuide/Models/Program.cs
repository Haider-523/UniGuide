using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace UniGuide.Models
{
    public class Program
    {
        public int ProgramID { get; set; }
        public int UniversityID { get; set; }
        public University? University { get; set; }
        public string? ProgramName { get; set; }
        public int DurationYears { get; set; }
        public decimal FeePerSemester { get; set; }
        public double MinMeritPercent { get; set; }
        public string? RequiredInterGroup { get; set; }
        public int TotalSeats { get; set; }
        public string? Shift { get; set; }

        public ICollection<CareerPath>? CareerPaths { get; set; }
        public ICollection<SavedUniversity>? SavedUniversities { get; set; }
        public ICollection<AdmissionDeadLine>? AdmissionDeadlines { get; set; }

    }
}
