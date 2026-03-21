namespace UniGuide.Models
{
    public class QuizViewModel
    {
        public List<QuizQuestion>? Questions { get; set; }
        public Dictionary<int, string>? Answers { get; set; }
        public string? ResultProfile { get; set; }
        public string? RecommendedField { get; set; }
        public bool ShowResults { get; set; } = false;
    }
}
