using Microsoft.EntityFrameworkCore;
using UniGuide.Models;

namespace UniGuide.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<UniGuide.Models.Program> Programs { get; set; }  // ✅ full namespace
        public DbSet<CareerPath> CareerPaths { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<StudentQuizResult> StudentQuizResults { get; set; }
        public DbSet<SavedUniversity> SavedUniversities { get; set; }
        public DbSet<AdmissionDeadLine> AdmissionDeadlines { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // University → Programs
            modelBuilder.Entity<UniGuide.Models.Program>()
                .HasOne(p => p.University)
                .WithMany(u => u.Programs)
                .HasForeignKey(p => p.UniversityID);

            // Keys
            modelBuilder.Entity<CareerPath>().HasKey(c => c.CareerID);
            modelBuilder.Entity<QuizQuestion>().HasKey(q => q.QuestionID);
            modelBuilder.Entity<SavedUniversity>().HasKey(s => s.SavedID);
            modelBuilder.Entity<StudentQuizResult>().HasKey(r => r.ResultID);
            modelBuilder.Entity<AdmissionDeadLine>().HasKey(a => a.DeadLineID);

            // Program → CareerPaths
            modelBuilder.Entity<CareerPath>()
                .HasOne(c => c.Program)
                .WithMany(p => p.CareerPaths)
                .HasForeignKey(c => c.ProgramID)
                .OnDelete(DeleteBehavior.Cascade);

            // Program → AdmissionDeadlines
            modelBuilder.Entity<AdmissionDeadLine>()
                .HasOne(a => a.Program)
                .WithMany(p => p.AdmissionDeadlines)
                .HasForeignKey(a => a.ProgramID)
                .OnDelete(DeleteBehavior.Cascade);

            // University → AdmissionDeadlines
            modelBuilder.Entity<AdmissionDeadLine>()
                .HasOne(a => a.University)
                .WithMany(u => u.AdmissionDeadLines)
                .HasForeignKey(a => a.UniversityID)
                .OnDelete(DeleteBehavior.Restrict);

            // Student → SavedUniversities
            modelBuilder.Entity<SavedUniversity>()
                .HasOne(s => s.Student)
                .WithMany(st => st.SavedUniversities)
                .HasForeignKey(s => s.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            // Program → SavedUniversities
            modelBuilder.Entity<SavedUniversity>()
                .HasOne(s => s.Program)
                .WithMany(p => p.SavedUniversities)
                .HasForeignKey(s => s.ProgramID)
                .OnDelete(DeleteBehavior.Restrict);

            // University → SavedUniversities
            modelBuilder.Entity<SavedUniversity>()
                .HasOne(s => s.University)
                .WithMany(u => u.SavedUniversities)
                .HasForeignKey(s => s.UniversityID)
                .OnDelete(DeleteBehavior.Restrict);

            // Student → QuizResults
            modelBuilder.Entity<StudentQuizResult>()
                .HasOne(q => q.Student)
                .WithMany(s => s.QuizResults)
                .HasForeignKey(q => q.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            // Decimal precision
            modelBuilder.Entity<UniGuide.Models.Program>()
                .Property(p => p.FeePerSemester)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<CareerPath>()
                .Property(c => c.MinSalaryPKR)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<CareerPath>()
                .Property(c => c.MaxSalaryPKR)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Student>()
                .Property(s => s.BudgetPerSemester)
                .HasColumnType("decimal(10,2)");
        }
    }
}
