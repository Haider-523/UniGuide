using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniGuide.Data;
using UniGuide.Models;
using System.Security.Cryptography;
using System.Text;

namespace UniGuide.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register() => View();

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            // Check if email already exists
            var exists = await _context.Students
                .AnyAsync(s => s.Email == model.Email);

            if (exists)
            {
                ModelState.AddModelError("Email", "Email already registered.");
                return View(model);
            }

            var student = new Student
            {
                FullName = model.FullName,
                Email = model.Email,
                InterGroup = model.InterGroup,
                InterMarksPercent = model.InterMarksPercent,
                City = model.City,
                BudgetPerSemester = model.BudgetPerSemester,
                PasswordHash = HashPassword(model.Password!),
                CreatedAt = DateTime.Now
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // Save to session
            HttpContext.Session.SetInt32("StudentID", student.StudentID);
            HttpContext.Session.SetString("StudentName", student.FullName!);

            return RedirectToAction("Index", "Home");
        }

        // GET: Login
        public IActionResult Login() => View();

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Email == model.Email
                    && s.PasswordHash == HashPassword(model.Password!));

            if (student == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View(model);
            }

            HttpContext.Session.SetInt32("StudentID", student.StudentID);
            HttpContext.Session.SetString("StudentName", student.FullName!);

            return RedirectToAction("Index", "Home");
        }

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // Simple password hashing
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}