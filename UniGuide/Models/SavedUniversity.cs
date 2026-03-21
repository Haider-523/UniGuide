using System.ComponentModel.DataAnnotations;

namespace UniGuide.Models
{
    public class SavedUniversity
    {
        [Key]
        public int SavedID { get; set; }
        public int StudentID { get; set; }
        public Student? Student { get; set; }
        public int UniversityID { get; set; }
        public University? University { get; set; }
        public int ProgramID { get; set; }
        public Program? Program { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.Now;
        public string? Notes { get; set; }
    }
}
