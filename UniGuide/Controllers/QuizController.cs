using Microsoft.AspNetCore.Mvc;
using UniGuide.Data;
using UniGuide.Models;
using UniGuide.Services;

namespace UniGuide.Controllers
{
    public class QuizController : Controller
    {
        private readonly QuizService _quizService;
        private readonly AppDbContext _context;

        public QuizController(QuizService quizService, AppDbContext context)
        {
            _quizService = quizService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var questions = await _quizService.GetAllQuestions();
            var model = new QuizViewModel { Questions = questions };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(IFormCollection form)
        {
            var answers = new Dictionary<int, string>();

            foreach (var key in form.Keys)
            {
                if (key.StartsWith("answers["))
                {
                    var id = int.Parse(key.Replace("answers[", "").Replace("]", ""));
                    answers[id] = form[key]!;
                }
            }

            var questions = await _quizService.GetAllQuestions();
            var profile = _quizService.CalculateProfile(answers);
            var recommended = _quizService.GetRecommendedField(profile);
            var breakdown = _quizService.GetScoreBreakdown(answers);

            var studentId = HttpContext.Session.GetInt32("StudentID");
            if (studentId != null)
            {
                var result = new StudentQuizResult
                {
                    StudentID = studentId.Value,
                    InterestProfile = profile,
                    RecommendedField = recommended,
                    ScoreBreakDown = breakdown,
                    TakenAt = DateTime.Now
                };
                _context.StudentQuizResults.Add(result);
                await _context.SaveChangesAsync();
            }

            var model = new QuizViewModel
            {
                Questions = questions,
                ResultProfile = profile,
                RecommendedField = recommended,
                ShowResults = true
            };

            return View("Result", model);
        }
    }
}