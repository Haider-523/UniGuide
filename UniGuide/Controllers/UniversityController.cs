using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGuide.Data;
using UniGuide.Models;

namespace UniGuide.Controllers
{
    public class UniversityController : Controller
    {
        private readonly AppDbContext _context;

        public UniversityController(AppDbContext context)
        {
            _context = context;
        }

        // Show all universities
        public async Task<IActionResult> Index()
        {
            var universities = await _context.Universities
                .Include(u => u.Programs)
                .ToListAsync();
            return View(universities);
        }

        // Show single university details
        public async Task<IActionResult> Details(int id)
        {
            var university = await _context.Universities
                .Include(u => u.Programs)
                    .ThenInclude(p => p.CareerPaths)
                .FirstOrDefaultAsync(u => u.UniversityID == id);

            if (university == null) return NotFound();

            return View(university);
        }

        [HttpPost]
        public async Task<IActionResult> Save(int universityId, int programId)
        {
            var studentId = HttpContext.Session.GetInt32("StudentID");
            if (studentId == null)
                return RedirectToAction("Login", "Account");

            // Check if already saved
            var exists = await _context.SavedUniversities
                .AnyAsync(s => s.StudentID == studentId
                    && s.UniversityID == universityId
                    && s.ProgramID == programId);

            if (!exists)
            {
                var saved = new SavedUniversity
                {
                    StudentID = studentId.Value,
                    UniversityID = universityId,
                    ProgramID = programId,
                    SavedAt = DateTime.Now
                };
                _context.SavedUniversities.Add(saved);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Details", new { id = universityId });
        }

        [HttpPost]
        public async Task<IActionResult> Unsave(int savedId)
        {
            var saved = await _context.SavedUniversities.FindAsync(savedId);
            if (saved != null)
            {
                _context.SavedUniversities.Remove(saved);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Dashboard");
        }

    }
}
