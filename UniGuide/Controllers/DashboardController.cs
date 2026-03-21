using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGuide.Data;
using UniGuide.Models;

namespace UniGuide.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Redirect to login if not logged in
            var studentId = HttpContext.Session.GetInt32("StudentID");
            if (studentId == null)
                return RedirectToAction("Login", "Account");

            var student = await _context.Students
                .Include(s => s.QuizResults)
                .Include(s => s.SavedUniversities!)
                    .ThenInclude(su => su.University)
                .Include(s => s.SavedUniversities!)
                    .ThenInclude(su => su.Program)
                .FirstOrDefaultAsync(s => s.StudentID == studentId);

            if (student == null)
                return RedirectToAction("Login", "Account");

            return View(student);
        }
    }
}