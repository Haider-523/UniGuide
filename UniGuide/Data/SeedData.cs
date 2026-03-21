//using Microsoft.EntityFrameworkCore;
//using UniGuide.Models;

//namespace UniGuide.Data
//{
//    public static class SeedData
//    {
//        public static void Initialize(AppDbContext context)
//        {
//            // Force delete and reseed every time (for debugging only)
//            context.Database.ExecuteSqlRaw("DELETE FROM CareerPaths");
//            context.Database.ExecuteSqlRaw("DELETE FROM AdmissionDeadLines");
//            context.Database.ExecuteSqlRaw("DELETE FROM SavedUniversities");
//            context.Database.ExecuteSqlRaw("DELETE FROM StudentQuizResults");
//            context.Database.ExecuteSqlRaw("DELETE FROM Programs");
//            context.Database.ExecuteSqlRaw("DELETE FROM Universities");

//            Console.WriteLine(">>> Starting seed...");

//            var uni1 = new University
//            {
//                Name = "FAST National University",
//                City = "Lahore",
//                Type = "Private",
//                HECRanking = 4,
//                EstablishedYear = 2000,
//                Website = "https://www.nu.edu.pk",
//                HasHostel = true,
//                HasTransport = false,
//                Description = "Top CS university in Pakistan"
//            };

//            var uni2 = new University
//            {
//                Name = "University of Engineering & Technology",
//                City = "Lahore",
//                Type = "Public",
//                HECRanking = 3,
//                EstablishedYear = 1921,
//                Website = "https://www.uet.edu.pk",
//                HasHostel = true,
//                HasTransport = true,
//                Description = "Premier engineering university"
//            };

//            var uni3 = new University
//            {
//                Name = "COMSATS University",
//                City = "Lahore",
//                Type = "Public",
//                HECRanking = 6,
//                EstablishedYear = 1998,
//                Website = "https://www.comsats.edu.pk",
//                HasHostel = true,
//                HasTransport = true,
//                Description = "Strong in CS and Engineering"
//            };

//            context.Universities.Add(uni1);
//            context.Universities.Add(uni2);
//            context.Universities.Add(uni3);

//            // ✅ Save universities FIRST to get their IDs
//            context.SaveChanges();
//            Console.WriteLine(">>> Universities saved. IDs: "
//                + uni1.UniversityID + ", "
//                + uni2.UniversityID + ", "
//                + uni3.UniversityID);

//            var prog1 = new UniGuide.Models.Program
//            {
//                UniversityID = uni1.UniversityID,
//                ProgramName = "BS Computer Science",
//                DurationYears = 4,
//                FeePerSemester = 95000,
//                MinMeritPercent = 70,
//                RequiredInterGroup = "FSc-PreEngineering,ICS",
//                TotalSeats = 120,
//                Shift = "Morning"
//            };

//            var prog2 = new UniGuide.Models.Program
//            {
//                UniversityID = uni2.UniversityID,
//                ProgramName = "BS Computer Engineering",
//                DurationYears = 4,
//                FeePerSemester = 35000,
//                MinMeritPercent = 75,
//                RequiredInterGroup = "FSc-PreEngineering",
//                TotalSeats = 100,
//                Shift = "Morning"
//            };

//            var prog3 = new UniGuide.Models.Program
//            {
//                UniversityID = uni3.UniversityID,
//                ProgramName = "BS Computer Science",
//                DurationYears = 4,
//                FeePerSemester = 42000,
//                MinMeritPercent = 60,
//                RequiredInterGroup = "FSc-PreEngineering,ICS",
//                TotalSeats = 150,
//                Shift = "Morning"
//            };

//            context.Programs.Add(prog1);
//            context.Programs.Add(prog2);
//            context.Programs.Add(prog3);

//            // ✅ Save programs to get their IDs
//            context.SaveChanges();
//            Console.WriteLine(">>> Programs saved. IDs: "
//                + prog1.ProgramID + ", "
//                + prog2.ProgramID + ", "
//                + prog3.ProgramID);

//            var career1 = new CareerPath
//            {
//                ProgramID = prog1.ProgramID,
//                JobTitle = "Software Engineer",
//                MinSalaryPKR = 80000,
//                MaxSalaryPKR = 200000,
//                IndustryType = "IT",
//                DemandLevel = "High",
//                SkillRequired = "C#, Java, Python, SQL"
//            };

//            var career2 = new CareerPath
//            {
//                ProgramID = prog1.ProgramID,
//                JobTitle = "Machine Learning Engineer",
//                MinSalaryPKR = 100000,
//                MaxSalaryPKR = 250000,
//                IndustryType = "IT",
//                DemandLevel = "High",
//                SkillRequired = "Python, TensorFlow, Data Science"
//            };

//            context.CareerPaths.Add(career1);
//            context.CareerPaths.Add(career2);

//            context.SaveChanges();
//            Console.WriteLine(">>> CareerPaths saved successfully!");
//            Console.WriteLine(">>> SEED COMPLETE ✅");
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using UniGuide.Models;

namespace UniGuide.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            // Only seed if empty — never delete existing data
            if (context.Universities.Any()) return;

            var uni1 = new University { Name = "FAST National University", City = "Lahore", Type = "Private", HECRanking = 4, EstablishedYear = 2000, Website = "https://www.nu.edu.pk", HasHostel = true, HasTransport = false, Description = "Top CS university in Pakistan", LogoURL = "" };
            var uni2 = new University { Name = "University of Engineering & Technology", City = "Lahore", Type = "Public", HECRanking = 3, EstablishedYear = 1921, Website = "https://www.uet.edu.pk", HasHostel = true, HasTransport = true, Description = "Premier engineering university", LogoURL = "" };
            var uni3 = new University { Name = "COMSATS University", City = "Lahore", Type = "Public", HECRanking = 6, EstablishedYear = 1998, Website = "https://www.comsats.edu.pk", HasHostel = true, HasTransport = true, Description = "Strong in CS and Engineering", LogoURL = "" };
            var uni4 = new University { Name = "LUMS", City = "Lahore", Type = "Private", HECRanking = 1, EstablishedYear = 1984, Website = "https://www.lums.edu.pk", HasHostel = true, HasTransport = false, Description = "Top ranked university in Pakistan", LogoURL = "" };
            var uni5 = new University { Name = "University of the Punjab", City = "Lahore", Type = "Public", HECRanking = 8, EstablishedYear = 1882, Website = "https://www.pu.edu.pk", HasHostel = true, HasTransport = true, Description = "Oldest university in Pakistan", LogoURL = "" };

            context.Universities.AddRange(uni1, uni2, uni3, uni4, uni5);
            context.SaveChanges();
        }
    }
}