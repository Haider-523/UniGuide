namespace UniGuide.Models
{
    public class MatchViewModel
    {
        public string? InterGroup { get; set; }
        public double Marks { get; set; }
        public string? City { get; set; }
        public decimal Budget { get; set; }
        public List<UniGuide.Models.Program>? MatchedPrograms { get; set; }
        public bool SearchPerformed { get; set; } = false;

        // Track which programs are already saved
        public List<int> SavedProgramIds { get; set; } = new();
        
    }
}
