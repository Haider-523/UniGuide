using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace UniGuide.Models
{
    public class University
    {
        public int UniversityID { get; set; }
        [Required, MaxLength(150)]
        public string? Name { get; set; }
        [Required]
        public string? City  { get; set; }
        public string? Type { get; set; }
        public int HECRanking { get; set; }
        public int EstablishedYear { get; set; }
        public string Website { get; set; }
        public bool HasHostel { get; set; }
        public bool HasTransport { get; set; }
        public string LogoURL { get; set; }
        public string Description { get; set; }
        public ICollection<Program> Programs { get; set; }
        public ICollection<AdmissionDeadLine>? AdmissionDeadLines { get; set; }
        public ICollection<SavedUniversity>? SavedUniversities { get; set; }
    }
}
