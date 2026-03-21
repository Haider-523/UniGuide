using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGuide.Data;
using UniGuide.Models;
using UniGuide.Services;

namespace UniGuide.Controllers
{
    public class MatchController : Controller
    {
        private readonly MatchingService _matchingService;
        private readonly AppDbContext _context;

        public MatchController(MatchingService matchingService, AppDbContext context)
        {
            _matchingService = matchingService;
            _context = context;
        }

        // GET — also handles redirect back from Save
        public async Task<IActionResult> Index(
            string? interGroup, double marks,
            string? city, decimal budget)
        {
            var model = new MatchViewModel
            {
                InterGroup = interGroup,
                Marks = marks,
                City = city,
                Budget = budget
            };

            // Auto-run search if params exist
            if (!string.IsNullOrEmpty(interGroup) && marks > 0
                && !string.IsNullOrEmpty(city) && budget > 0)
            {
                model.MatchedPrograms = await _matchingService
                    .GetMatchedPrograms(interGroup, marks, city, budget);
                model.SearchPerformed = true;

                var studentId = HttpContext.Session.GetInt32("StudentID");
                if (studentId != null)
                {
                    model.SavedProgramIds = await _context.SavedUniversities
                        .Where(s => s.StudentID == studentId)
                        .Select(s => s.ProgramID)
                        .ToListAsync();
                }
            }

            return View(model);
        }

        // POST — form submission
        [HttpPost]
        public async Task<IActionResult> Index(MatchViewModel model)
        {
            model.MatchedPrograms = await _matchingService.GetMatchedPrograms(
                model.InterGroup!,
                model.Marks,
                model.City!,
                model.Budget
            );
            model.SearchPerformed = true;

            var studentId = HttpContext.Session.GetInt32("StudentID");
            if (studentId != null)
            {
                model.SavedProgramIds = await _context.SavedUniversities
                    .Where(s => s.StudentID == studentId)
                    .Select(s => s.ProgramID)
                    .ToListAsync();
            }

            return View(model);
        }
    }
}