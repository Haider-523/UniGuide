using UniGuide.Data;
using UniGuide.Models;
using Microsoft.EntityFrameworkCore;

namespace UniGuide.Services
{
    public class QuizService
    {
        private readonly AppDbContext _context;

        public QuizService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<QuizQuestion>> GetAllQuestions()
        {
            return await _context.QuizQuestions.ToListAsync();
        }

        // A=Tech, B=Medical, C=Business, D=Arts
        public string CalculateProfile(Dictionary<int, string> answers)
        {
            int tech = 0, medical = 0, business = 0, arts = 0;

            foreach (var answer in answers.Values)
            {
                if (answer == "A") tech++;
                else if (answer == "B") medical++;
                else if (answer == "C") business++;
                else if (answer == "D") arts++;
            }

            int max = Math.Max(Math.Max(tech, medical), Math.Max(business, arts));

            if (max == tech) return "Tech-Oriented";
            if (max == medical) return "Medical-Oriented";
            if (max == business) return "Business-Oriented";
            return "Arts-Oriented";
        }

        public string GetRecommendedField(string profile)
        {
            return profile switch
            {
                "Tech-Oriented" => "Computer Science, Software Engineering, IT, Electrical Engineering",
                "Medical-Oriented" => "MBBS, Pharmacy, Bioinformatics, Biotechnology, Dentistry",
                "Business-Oriented" => "BBA, Accounting & Finance, Economics, Commerce",
                "Arts-Oriented" => "Mass Communication, Psychology, English Literature, Education",
                _ => "General Sciences"
            };
        }

        public string GetScoreBreakdown(Dictionary<int, string> answers)
        {
            int tech = 0, medical = 0, business = 0, arts = 0;
            foreach (var answer in answers.Values)
            {
                if (answer == "A") tech++;
                else if (answer == "B") medical++;
                else if (answer == "C") business++;
                else if (answer == "D") arts++;
            }
            return $"{{\"Tech\":{tech},\"Medical\":{medical},\"Business\":{business},\"Arts\":{arts}}}";
        }
    }
}