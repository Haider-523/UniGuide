using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGuide.Data;
using UniGuide.Models;
using System.Security.Cryptography;
using System.Text;

namespace UniGuide.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // ── Auth ─────────────────────────────────────
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var admin = await _context.Admins
                .FirstOrDefaultAsync(a =>
                    a.Email == model.Email &&
                    a.PasswordHash == HashPassword(model.Password!));

            if (admin == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            HttpContext.Session.SetString("AdminName", admin.FullName!);
            HttpContext.Session.SetInt32("AdminID", admin.AdminID);
            return RedirectToAction("Dashboard");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminName");
            HttpContext.Session.Remove("AdminID");
            return RedirectToAction("Login");
        }

        // ── Dashboard ─────────────────────────────────
        public async Task<IActionResult> Dashboard()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
                return RedirectToAction("Login");

            ViewBag.UniversityCount = await _context.Universities.CountAsync();
            ViewBag.ProgramCount = await _context.Programs.CountAsync();
            ViewBag.StudentCount = await _context.Students.CountAsync();
            ViewBag.QuizCount = await _context.QuizQuestions.CountAsync();

            return View();
        }

        // ── Universities ──────────────────────────────
        public async Task<IActionResult> Universities()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
                return RedirectToAction("Login");

            var unis = await _context.Universities
                .Include(u => u.Programs)
                .OrderBy(u => u.HECRanking)
                .ToListAsync();
            return View(unis);
        }

        public IActionResult AddUniversity()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
                return RedirectToAction("Login");
            return View(new University());
        }

        [HttpPost]
        public async Task<IActionResult> AddUniversity(University model)
        {
            // Remove validation for optional fields
            ModelState.Remove("LogoURL");
            ModelState.Remove("Programs");
            ModelState.Remove("AdmissionDeadlines");
            ModelState.Remove("SavedUniversities");

            if (!ModelState.IsValid)
            {
                // Show what's failing
                foreach (var error in ModelState.Values
                    .SelectMany(v => v.Errors))
                {
                    Console.WriteLine("Validation error: " + error.ErrorMessage);
                }
                return View(model);
            }

            model.LogoURL = model.LogoURL ?? "";
            _context.Universities.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Universities");
        }

        public async Task<IActionResult> EditUniversity(int id)
        {
            if (HttpContext.Session.GetString("AdminName") == null)
                return RedirectToAction("Login");
            var uni = await _context.Universities.FindAsync(id);
            if (uni == null) return NotFound();
            return View(uni);
        }

        [HttpPost]
        public async Task<IActionResult> EditUniversity(University model)
        {
            ModelState.Remove("LogoURL");
            ModelState.Remove("Programs");
            ModelState.Remove("AdmissionDeadlines");
            ModelState.Remove("SavedUniversities");

            if (!ModelState.IsValid) return View(model);

            model.LogoURL = model.LogoURL ?? "";
            _context.Universities.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Universities");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUniversity(int id)
        {
            var uni = await _context.Universities.FindAsync(id);
            if (uni != null)
            {
                _context.Universities.Remove(uni);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Universities");
        }

        // ── Programs ──────────────────────────────────
        public async Task<IActionResult> Programs()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
                return RedirectToAction("Login");

            var programs = await _context.Programs
                .Include(p => p.University)
                .OrderBy(p => p.University!.Name)
                .ToListAsync();
            return View(programs);
        }

        public async Task<IActionResult> AddProgram()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
                return RedirectToAction("Login");

            ViewBag.Universities = await _context.Universities
                .OrderBy(u => u.Name).ToListAsync();
            return View(new UniGuide.Models.Program());
        }

        [HttpPost]
        public async Task<IActionResult> AddProgram(UniGuide.Models.Program model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Universities = await _context.Universities
                    .OrderBy(u => u.Name).ToListAsync();
                return View(model);
            }
            _context.Programs.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Programs");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            var program = await _context.Programs.FindAsync(id);
            if (program != null)
            {
                _context.Programs.Remove(program);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Programs");
        }

        // ── Students ──────────────────────────────────
        public async Task<IActionResult> Students()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
                return RedirectToAction("Login");

            var students = await _context.Students
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
            return View(students);
        }

        // ── Quiz Questions ────────────────────────────
        public async Task<IActionResult> QuizQuestions()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
                return RedirectToAction("Login");

            var questions = await _context.QuizQuestions.ToListAsync();
            return View(questions);
        }

        // GET
        public IActionResult AddQuestion()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
                return RedirectToAction("Login");
            return View(new QuizQuestion());
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> AddQuestion(QuizQuestion model)
        {
            ModelState.Remove("Category");
            if (!ModelState.IsValid) return View(model);
            _context.QuizQuestions.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("QuizQuestions");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var q = await _context.QuizQuestions.FindAsync(id);
            if (q != null)
            {
                _context.QuizQuestions.Remove(q);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("QuizQuestions");
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public IActionResult Setup()
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes("admin123"));
            var hash = Convert.ToBase64String(bytes);

            var admin = new Admin
            {
                FullName = "Admin",
                Email = "admin@uniguide.com",
                PasswordHash = hash,
                CreatedAt = DateTime.Now
            };

            _context.Admins.Add(admin);
            _context.SaveChanges();

            return Content("Admin created! Hash: " + hash);
        }

        // GET: Career Paths list
        public async Task<IActionResult> CareerPaths()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
                return RedirectToAction("Login");

            var careers = await _context.CareerPaths
                .Include(c => c.Program)
                    .ThenInclude(p => p!.University)
                .OrderBy(c => c.Program!.University!.Name)
                .ToListAsync();
            return View(careers);
        }

        // GET: Add Career Path
        public async Task<IActionResult> AddCareerPath()
        {
            if (HttpContext.Session.GetString("AdminName") == null)
                return RedirectToAction("Login");

            ViewBag.Programs = await _context.Programs
                .Include(p => p.University)
                .OrderBy(p => p.University!.Name)
                .ToListAsync();
            return View(new CareerPath());
        }

        // POST: Add Career Path
        [HttpPost]
        public async Task<IActionResult> AddCareerPath(CareerPath model)
        {
            ModelState.Remove("Program");
            if (!ModelState.IsValid)
            {
                ViewBag.Programs = await _context.Programs
                    .Include(p => p.University)
                    .OrderBy(p => p.University!.Name)
                    .ToListAsync();
                return View(model);
            }
            _context.CareerPaths.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("CareerPaths");
        }

        // POST: Delete Career Path
        [HttpPost]
        public async Task<IActionResult> DeleteCareerPath(int id)
        {
            var career = await _context.CareerPaths.FindAsync(id);
            if (career != null)
            {
                _context.CareerPaths.Remove(career);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("CareerPaths");
        }

    }
}