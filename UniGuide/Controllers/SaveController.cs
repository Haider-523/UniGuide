using Microsoft.AspNetCore.Mvc;
using UniGuide.Data;
using UniGuide.Models;
using Microsoft.EntityFrameworkCore;

namespace UniGuide.Controllers
{
    public class SaveController : Controller
    {
        private readonly AppDbContext _context;

        public SaveController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int universityId, int programId,
                string? interGroup, double marks, string? city, decimal budget)
        {
            var studentId = HttpContext.Session.GetInt32("StudentID");

            if (studentId == null)
                return Json(new { success = false, message = "Not logged in" });

            var existing = await _context.SavedUniversities
                .FirstOrDefaultAsync(s =>
                    s.StudentID == studentId &&
                    s.UniversityID == universityId &&
                    s.ProgramID == programId);

            if (existing != null)
            {
                _context.SavedUniversities.Remove(existing);
                await _context.SaveChangesAsync();
                return Json(new { success = true, saved = false });
            }
            else
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
                return Json(new { success = true, saved = true });
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Toggle(int universityId, int programId)
        //{
        //    var studentId = HttpContext.Session.GetInt32("StudentID");

        //    if (studentId == null)
        //        return RedirectToAction("Login", "Account");

        //    var existing = await _context.SavedUniversities
        //        .FirstOrDefaultAsync(s =>
        //            s.StudentID == studentId &&
        //            s.UniversityID == universityId &&
        //            s.ProgramID == programId);

        //    if (existing != null)
        //    {
        //        // Already saved — remove it
        //        _context.SavedUniversities.Remove(existing);
        //        await _context.SaveChangesAsync();
        //    }
        //    else
        //    {
        //        // Not saved — save it
        //        var saved = new SavedUniversity
        //        {
        //            StudentID = studentId.Value,
        //            UniversityID = universityId,
        //            ProgramID = programId,
        //            SavedAt = DateTime.Now
        //        };
        //        _context.SavedUniversities.Add(saved);
        //        await _context.SaveChangesAsync();
        //    }

        //    return RedirectToAction("Index", "Match");
        //}

        [HttpPost]
        public async Task<IActionResult> Remove(int savedId)
        {
            var studentId = HttpContext.Session.GetInt32("StudentID");
            if (studentId == null)
                return RedirectToAction("Login", "Account");

            var saved = await _context.SavedUniversities
                .FirstOrDefaultAsync(s =>
                    s.SavedID == savedId &&
                    s.StudentID == studentId);

            if (saved != null)
            {
                _context.SavedUniversities.Remove(saved);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Dashboard");
        }
    }
}