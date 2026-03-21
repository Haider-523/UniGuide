using Microsoft.EntityFrameworkCore;
using UniGuide.Data;
using UniGuide.Models;

namespace UniGuide.Services
{
    public class MatchingService
    {
        private readonly AppDbContext _context;

        public MatchingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UniGuide.Models.Program>> GetMatchedPrograms(
            string interGroup,
            double marks,
            string city,
            decimal budget)
        {
            var allPrograms = await _context.Programs
                .Include(p => p.University)
                .Include(p => p.CareerPaths)
                .ToListAsync();

            var matched = allPrograms.Where(p =>

                // Merit check
                marks >= p.MinMeritPercent &&

                // Budget check
                p.FeePerSemester <= budget &&

                // Inter group check
                p.RequiredInterGroup != null &&
                p.RequiredInterGroup
                    .Split(',')
                    .Any(g => g.Trim()
                    .Equals(interGroup.Trim(),
                    StringComparison.OrdinalIgnoreCase)) &&

                // City check (if same city OR university accepts all)
                (p.University!.City.Equals(city.Trim(),
                    StringComparison.OrdinalIgnoreCase))

            )
            .OrderBy(p => p.University!.HECRanking)
            .ToList();

            return matched;
        }
    }
}
