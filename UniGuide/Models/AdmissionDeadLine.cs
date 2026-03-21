using System.ComponentModel.DataAnnotations;

namespace UniGuide.Models
{
    public class AdmissionDeadLine
    {
        [Key]
        public int DeadLineID { get; set; }
        public DateTime AdmissionOpenDate { get; set; }
        public DateTime LastDate { get; set; }
        public bool TestRequired { get; set; }
        public DateTime? TestDate { get; set; }
        public int UniversityID { get; set; }
        public University? University { get; set; }
        public int ProgramID { get; set; }
        public Program? Program { get; set; }
    }
}
